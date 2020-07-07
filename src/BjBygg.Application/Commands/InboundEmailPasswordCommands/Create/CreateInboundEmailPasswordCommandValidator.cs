using FluentValidation;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.Create
{
    public class CreateInboundEmailPasswordCommandValidator : AbstractValidator<CreateInboundEmailPasswordCommand>
    {
        public CreateInboundEmailPasswordCommandValidator()
        {
            RuleFor(v => v.Password)
               .NotEmpty()
               .MaximumLength(45);
        }
    }
}
