using FluentValidation;

namespace BjBygg.Application.Identity.Commands.IdentityCommands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(v => v.AccessToken)
                .NotEmpty();

            RuleFor(v => v.RefreshToken)
                .NotEmpty();

            RuleFor(v => v.SigningKey)
                .NotEmpty();
        }
    }
}
