using AutoMapper;
using CleanArchitecture.Core.Entities;
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
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CreateUserHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Not allowing new leaders
            if (request.Role.ToLower() == "Leder") 
                return new CreateUserResponse(false, $"It's forbidden to add users with the role {request.Role}");

            var user = _mapper.Map<ApplicationUser>(request);                  

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return _mapper.Map<CreateUserResponse>(result);

            await _userManager.AddToRoleAsync(user, request.Role);

            return _mapper.Map<CreateUserResponse>(result);
        }
    }
}
