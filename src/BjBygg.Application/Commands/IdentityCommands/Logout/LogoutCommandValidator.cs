using FluentValidation;

namespace BjBygg.Application.Commands.IdentityCommands.Logout
{
    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(v => v.RefreshToken)
               .NotEmpty();
        }
    }
}
