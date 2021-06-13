using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.UserCommands.Create;
using BjBygg.Application.Identity.Common.Models;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserCommands.NewPassword
{
    public class NewPasswordCommandHandler : IRequestHandler<NewPasswordCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public NewPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(NewPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null) throw new EntityNotFoundException(nameof(ApplicationUser), request.UserName);

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (ForbiddenRoles.Value.Contains(currentRole))
                throw new ForbiddenException();

            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var validationResult = await passwordValidator.ValidateAsync(_userManager, user, request.NewPassword);

            if (!validationResult.Succeeded)
            {
                var validationErrors = validationResult.Errors.Select(x => new ValidationFailure(x.Code, x.Description));
                throw new ValidationException(validationErrors);
            }

            var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
            user.PasswordHash = newPasswordHash;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                var validationErrors = updateResult.Errors.Select(x => new ValidationFailure(x.Code, x.Description));
                throw new ValidationException(validationErrors);
            }

            return Unit.Value;
        }
    }
}
