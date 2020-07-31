using FluentValidation;

namespace BjBygg.Application.Application.Commands.EmployerCommands.Update
{
    public class UpdateEmployerCommandValidator : AbstractValidator<UpdateEmployerCommand>
    {
        public UpdateEmployerCommandValidator()
        {
            RuleFor(v => v.Id)
               .NotEmpty();

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
