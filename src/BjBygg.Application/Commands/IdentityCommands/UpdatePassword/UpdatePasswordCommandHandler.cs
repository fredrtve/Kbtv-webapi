using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.IdentityCommands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICurrentUserService _userService;

        public UpdatePasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICurrentUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await this._userManager.FindByNameAsync(_userService.UserName);

            if (user == null)
                throw new EntityNotFoundException($"Finner ikke bruker med brukernavn '{_userService.UserName}'.");

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!changePasswordResult.Succeeded)
                throw new BadRequestException("Feil passord!");

            await _signInManager.RefreshSignInAsync(user);

            return Unit.Value;
        }
    }
}
