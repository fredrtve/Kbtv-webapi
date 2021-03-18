using BjBygg.Core;
using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserCommands.NewPassword
{
    public class NewPasswordCommandValidator : AbstractValidator<NewPasswordCommand>
    {
        public NewPasswordCommandValidator()
        {
            RuleFor(v => v.NewPassword)
                .NotEmpty()
                .MinimumLength(ValidationRules.UserPasswordMinLength)
                .MaximumLength(ValidationRules.UserPasswordMaxLength)
                .WithName("Passord");

            RuleFor(v => v.UserName)
                .NotEmpty()
                .WithName("Brukernavn");
        }
    }
}
