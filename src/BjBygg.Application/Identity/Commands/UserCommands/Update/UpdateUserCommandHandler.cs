using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserCommands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if(!String.IsNullOrEmpty(request.Role))
            {
                if (request.Role == Roles.Leader) //Not allowing new leaders
                    throw new ForbiddenException();

                if (!Roles.All.Contains(request.Role))
                    throw new EntityNotFoundException(nameof(IdentityRole), request.Role);
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                throw new EntityNotFoundException(nameof(ApplicationUser), request.UserName);

            if (!String.IsNullOrEmpty(request.FirstName))
                user.FirstName = request.FirstName;

            if (!String.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;

            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;

            if (request.Role != Roles.Employer) user.EmployerId = null;
            else user.EmployerId = request.EmployerId;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new BadRequestException("Something went wrong when trying to update user");

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (!String.IsNullOrEmpty(request.Role))
            {
                //Not allowed to degrade role of leaders
                if (request.Role != Roles.Leader && currentRole == Roles.Leader)
                    throw new ForbiddenException();

                if (currentRole != request.Role && currentRole != Roles.Leader)
                {
                    await _userManager.RemoveFromRoleAsync(user, currentRole);
                    await _userManager.AddToRoleAsync(user, request.Role);
                }
            }

            return Unit.Value;
        }
    }
}
