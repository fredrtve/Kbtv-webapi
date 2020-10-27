using BjBygg.Application.Common.Validation;
using FluentValidation;
using System;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            Include(new ContactableValidator());
        }
    }
}
