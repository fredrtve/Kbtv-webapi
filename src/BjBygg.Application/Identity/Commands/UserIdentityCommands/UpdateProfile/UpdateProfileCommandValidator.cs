using FluentValidation;
using System;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(v => v.PhoneNumber)
                .MaximumLength(12)
                .WithName("Mobilnummer");

            RuleFor(v => v.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithName("Epost");
        }
    }
}
