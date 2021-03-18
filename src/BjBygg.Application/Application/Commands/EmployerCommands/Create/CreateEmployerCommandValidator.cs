using BjBygg.Application.Common.Validation;
using BjBygg.Core;
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
                .MaximumLength(ValidationRules.NameMaxLength)
                .WithName("Navn");

            Include(new ContactableValidator());

            RuleFor(v => v.Address)
                .MaximumLength(ValidationRules.AddressMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Address))
                .WithName("Adresse");
        }
    }
}
