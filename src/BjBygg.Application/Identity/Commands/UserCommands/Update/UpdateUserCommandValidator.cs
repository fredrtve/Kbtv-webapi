using BjBygg.Application.Common.Validation;
using CleanArchitecture.Core;
using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserCommands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .MaximumLength(ValidationRules.NameMaxLength)
                .WithName("Brukernavn");

            RuleFor(v => v.FirstName)
                .MaximumLength(ValidationRules.NameMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.FirstName))
                .WithName("Fornavn");

            RuleFor(v => v.LastName)
                .MaximumLength(ValidationRules.NameMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.LastName))
                .WithName("Etternavn");

            Include(new ContactableValidator());
        }
    }
}
