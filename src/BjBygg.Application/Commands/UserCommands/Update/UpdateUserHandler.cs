using AutoMapper;
using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.UserCommands.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UpdateUserHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Role == "Leder") //Not allowing new leaders
                throw new ForbiddenException($"Updating to role Leder is forbidden.");

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null) 
                throw new EntityNotFoundException($"User does not exist with username {request.UserName}"); ;
       
            if(!String.IsNullOrEmpty(request.FirstName)) 
                user.FirstName = request.FirstName;

            if (!String.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;

            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;

            if (request.Role != "Oppdragsgiver") user.EmployerId = null;
            else user.EmployerId = request.EmployerId;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) 
                throw new BadRequestException(result.Errors.ToString());

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (currentRole != request.Role && currentRole != "Leder" && !String.IsNullOrEmpty(request.Role))
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole);
                await _userManager.AddToRoleAsync(user, request.Role);
            }

            var response = _mapper.Map<UserDto>(user);
            response.Role = request.Role;
            return response;
        }
    }
}
