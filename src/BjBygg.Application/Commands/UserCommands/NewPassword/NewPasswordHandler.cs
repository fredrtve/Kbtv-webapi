using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.UserCommands.NewPassword
{
    public class NewPasswordHandler : IRequestHandler<NewPasswordCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public NewPasswordHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(NewPasswordCommand request, CancellationToken cancellationToken)
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

            return true;
        }
    }
}
