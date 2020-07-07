using FluentValidation;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.Verify
{
    public class VerifyInboundEmailPasswordCommandValidator : AbstractValidator<VerifyInboundEmailPasswordCommand>
    {
        public VerifyInboundEmailPasswordCommandValidator()
        {
            RuleFor(v => v.Password)
                .NotEmpty();
        }
    }
}
