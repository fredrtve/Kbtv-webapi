using CleanArchitecture.Core;
using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(v => v.NewPassword)
               .NotEmpty()
               .MinimumLength(ValidationRules.UserPasswordMinLength)
               .MaximumLength(ValidationRules.UserPasswordMaxLength)
               .WithName("Nytt Passord");

            RuleFor(v => v.OldPassword)
              .NotEmpty()
              .MaximumLength(ValidationRules.UserPasswordMaxLength)
              .WithName("Gammelt Passord");
        }
    }
}
