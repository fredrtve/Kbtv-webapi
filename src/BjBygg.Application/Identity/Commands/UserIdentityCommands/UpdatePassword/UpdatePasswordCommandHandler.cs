using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public UpdatePasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_currentUserService.UserName);

            if (user == null)
                throw new EntityNotFoundException(nameof(ApplicationUser), _currentUserService.UserName);

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!changePasswordResult.Succeeded)
                throw new BadRequestException("Something went wrong when trying to update password");

            //await _signInManager.RefreshSignInAsync(user);

            return Unit.Value;
        }
    }
}
