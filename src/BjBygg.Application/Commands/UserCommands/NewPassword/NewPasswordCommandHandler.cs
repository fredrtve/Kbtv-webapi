using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.UserCommands.NewPassword
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

            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var validationResult = await passwordValidator.ValidateAsync(_userManager, user, request.NewPassword);

            if (!validationResult.Succeeded)
                throw new BadRequestException("New password is invalid");

            var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
            user.PasswordHash = newPasswordHash;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new BadRequestException("Something went wrong when trying to set new password");

            return Unit.Value;
        }
    }
}
