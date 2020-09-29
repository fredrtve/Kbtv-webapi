using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(v => v.NewPassword)
               .NotEmpty()
               .MinimumLength(7)
               .MaximumLength(100)
               .WithName("Nytt Passord");

            RuleFor(v => v.OldPassword)
              .NotEmpty()
              .MaximumLength(100)
              .WithName("Gammelt Passord");
        }
    }
}
