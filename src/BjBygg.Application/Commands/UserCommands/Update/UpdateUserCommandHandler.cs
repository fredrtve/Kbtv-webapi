using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.UserCommands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Role == "Leder") //Not allowing new leaders
                throw new BadRequestException($"Cant create users with role '{request.Role}'");

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                throw new EntityNotFoundException(nameof(ApplicationUser), request.UserName);

            if (!String.IsNullOrEmpty(request.FirstName))
                user.FirstName = request.FirstName;

            if (!String.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;

            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;

            if (request.Role != "Oppdragsgiver") user.EmployerId = null;
            else user.EmployerId = request.EmployerId;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new BadRequestException("Something went wrong when trying to update user");

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
