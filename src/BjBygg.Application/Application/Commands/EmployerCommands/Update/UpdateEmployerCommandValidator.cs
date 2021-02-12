using BjBygg.Application.Common.Validation;
using CleanArchitecture.Core;
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
                    .MaximumLength(ValidationRules.NameMaxLength)
                    .WithName("Navn");

            RuleFor(v => v.Address)
                .MaximumLength(ValidationRules.AddressMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Address))
                .WithName("Adresse");

            Include(new ContactableValidator());
        }
    }
}
