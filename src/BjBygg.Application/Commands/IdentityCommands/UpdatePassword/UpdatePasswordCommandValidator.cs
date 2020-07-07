using FluentValidation;

namespace BjBygg.Application.Commands.IdentityCommands.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(v => v.NewPassword)
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(v => v.OldPassword)
              .NotEmpty()
              .MaximumLength(100);
        }
    }
}
