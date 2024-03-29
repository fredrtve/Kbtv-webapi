﻿using BjBygg.Core;
using BjBygg.SharedKernel;
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
                 .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                 .WithName("Mobilnummer");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithName("Epost");
        }
    }
}
