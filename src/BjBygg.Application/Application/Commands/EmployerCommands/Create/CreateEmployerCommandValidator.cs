using FluentValidation;

namespace BjBygg.Application.Application.Commands.EmployerCommands.Create
{
    public class CreateEmployerCommandValidator : AbstractValidator<CreateEmployerCommand>
    {
        public CreateEmployerCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(45)
                .WithName("Navn");

            RuleFor(v => v.PhoneNumber)
                .MaximumLength(12)
                .WithName("Mobilnummer");

            RuleFor(v => v.Address)
                .MaximumLength(100)
                .WithName("Adresse");

            RuleFor(v => v.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithName("Epost");
        }
    }
}
