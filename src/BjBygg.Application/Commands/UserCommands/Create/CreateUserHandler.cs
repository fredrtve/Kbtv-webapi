using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMapper _mapper;

        public CreateUserHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Not allowing new leaders
            if (request.Role.ToLower() == "Leder")
                throw new ForbiddenException($"Creating users with role {request.Role} is forbidden.");

            var user = _mapper.Map<ApplicationUser>(request);

            if (request.Role != "Oppdragsgiver") user.EmployerId = null;

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.ToString());

            await _userManager.AddToRoleAsync(user, request.Role);

            var response = _mapper.Map<UserDto>(user);
            response.Role = request.Role;

            return response;
        }
    }
}
