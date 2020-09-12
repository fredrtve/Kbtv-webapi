using FluentValidation;

namespace BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Create
{
    public class CreateInboundEmailPasswordCommandValidator : AbstractValidator<CreateInboundEmailPasswordCommand>
    {
        public CreateInboundEmailPasswordCommandValidator()
        {
            RuleFor(v => v.Id)
                 .NotEmpty();

            RuleFor(v => v.Password)
               .NotEmpty()
               .MaximumLength(45);
        }
    }
}
