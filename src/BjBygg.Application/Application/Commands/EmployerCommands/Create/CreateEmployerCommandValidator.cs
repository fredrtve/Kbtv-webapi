using FluentValidation;

namespace BjBygg.Application.Application.Commands.EmployerCommands.Create
{
    public class CreateEmployerCommandValidator : AbstractValidator<CreateEmployerCommand>
    {
        public CreateEmployerCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(45);

            RuleFor(v => v.PhoneNumber)
                .MaximumLength(12);

            RuleFor(v => v.Address)
                .MaximumLength(100);

            RuleFor(v => v.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}
