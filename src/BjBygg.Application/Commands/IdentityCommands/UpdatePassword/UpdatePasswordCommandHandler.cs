using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Identity;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.IdentityCommands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICurrentUserService _currentUserService;

        public UpdatePasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await this._userManager.FindByNameAsync(_currentUserService.UserName);

            if (user == null)
                throw new EntityNotFoundException(nameof(ApplicationUser), _currentUserService.UserName);

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
       
            if (!changePasswordResult.Succeeded)
                throw new BadRequestException("Something went wrong when trying to update password");

            await _signInManager.RefreshSignInAsync(user);

            return Unit.Value;
        }
    }
}
