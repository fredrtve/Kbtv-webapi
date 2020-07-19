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
               .MaximumLength(100);

            RuleFor(v => v.OldPassword)
              .NotEmpty()
              .MaximumLength(100);
        }
    }
}
