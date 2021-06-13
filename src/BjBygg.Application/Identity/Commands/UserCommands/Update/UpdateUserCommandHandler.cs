using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserCommands.Create;
using BjBygg.Application.Identity.Common.Models;
using FluentValidation.Results;
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
            if (!String.IsNullOrEmpty(request.Role))
            {
                if (ForbiddenRoles.Value.Contains(request.Role))
                    throw new ForbiddenException();

                if (!Roles.All.Contains(request.Role))
                    throw new EntityNotFoundException(nameof(IdentityRole), request.Role);
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                throw new EntityNotFoundException(nameof(ApplicationUser), request.UserName);

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (ForbiddenRoles.Value.Contains(currentRole))
                throw new ForbiddenException();

            if (!String.IsNullOrWhiteSpace(request.FirstName))
                user.FirstName = request.FirstName;

            if (!String.IsNullOrWhiteSpace(request.LastName))
                user.LastName = request.LastName;

            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var validationErrors = result.Errors.Select(x => new ValidationFailure(x.Code, x.Description));
                throw new ValidationException(validationErrors);
            }

            if (!String.IsNullOrEmpty(request.Role) && currentRole != request.Role)
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole);
                await _userManager.AddToRoleAsync(user, request.Role);
            }

            return Unit.Value;
        }
    }
}
