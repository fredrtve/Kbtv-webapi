using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
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

            if (user == null) throw new EntityNotFoundException($"Cant find user with username {request.UserName}");

            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var validationResult = await passwordValidator.ValidateAsync(_userManager, user, request.NewPassword);

            if (!validationResult.Succeeded)
                throw new BadRequestException(validationResult.Errors.FirstOrDefault().ToString());

            var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
            user.PasswordHash = newPasswordHash;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new BadRequestException(updateResult.Errors.FirstOrDefault().ToString());

            return Unit.Value;
        }
    }
}
