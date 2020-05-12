using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.UserCommands.Create
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserHandler(UserManager<ApplicationUser> userManager, AppDbContext dbContext, IMapper mapper)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Employer employer = null;
            //Not allowing new leaders
            if (request.Role.ToLower() == "leder")
                throw new ForbiddenException($"Creating users with role {request.Role} is forbidden.");
            else if (request.Role.ToLower() == "oppdragsgiver") 
            {
                if(request.EmployerId == null)
                    throw new BadRequestException($"Employer Id required for creating users with role = {request.Role}");
                employer = await _dbContext.Set<Employer>().FindAsync(request.EmployerId);
                if(employer == null)
                    throw new BadRequestException($"Employer not found with Id = {request.EmployerId}");
                else if (employer.UserName != null)
                    throw new BadRequestException($"Employer already attached to user = {employer.UserName}");
            }

            var user = _mapper.Map<ApplicationUser>(request);                  

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.ToString());

            await _userManager.AddToRoleAsync(user, request.Role);

            var response = _mapper.Map<UserDto>(user);
            response.Role = request.Role;

            if (employer != null) //If employer role and employer exist, add user id to employer
            {
                employer.UserName = user.UserName;
                _dbContext.Entry(employer).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            };
                  
            return response;
        }
    }
}
