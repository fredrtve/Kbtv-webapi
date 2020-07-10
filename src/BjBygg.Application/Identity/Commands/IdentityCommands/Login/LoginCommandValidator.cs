using FluentValidation;

namespace BjBygg.Application.Identity.Commands.IdentityCommands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(v => v.UserName)
               .NotEmpty();

            RuleFor(v => v.Password)
                .NotEmpty();
        }
    }
}
