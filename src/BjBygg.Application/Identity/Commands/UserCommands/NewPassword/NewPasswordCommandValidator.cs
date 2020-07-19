using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserCommands.NewPassword
{
    public class NewPasswordCommandValidator : AbstractValidator<NewPasswordCommand>
    {
        public NewPasswordCommandValidator()
        {
            RuleFor(v => v.NewPassword)
                .NotEmpty()
                .MinimumLength(7);

            RuleFor(v => v.UserName)
                .NotEmpty();
        }
    }
}
