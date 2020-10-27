using CleanArchitecture.Core;
using CleanArchitecture.SharedKernel;
using FluentValidation;

namespace BjBygg.Application.Common.Validation
{
    public class ContactableValidator : AbstractValidator<IContactable>
    {
        public ContactableValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .MinimumLength(ValidationRules.PhoneNumberMinLength)
                .MaximumLength(ValidationRules.PhoneNumberMaxLength)
                .WithName("Mobilnummer");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithName("Epost");
        }
    }
}
