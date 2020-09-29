using FluentValidation;

namespace BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Verify
{
    public class VerifyInboundEmailPasswordCommandValidator : AbstractValidator<VerifyInboundEmailPasswordCommand>
    {
        public VerifyInboundEmailPasswordCommandValidator()
        {
            RuleFor(v => v.Password)
                .NotEmpty()
                .WithName("Passord");
        }
    }
}
