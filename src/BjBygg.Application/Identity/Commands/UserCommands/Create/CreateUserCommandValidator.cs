using BjBygg.Application.Common.Validation;
using CleanArchitecture.Core;
using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserCommands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .MaximumLength(ValidationRules.NameMaxLength)
                .WithName("Brukernavn");

            RuleFor(v => v.FirstName)
                .NotEmpty()
                .MaximumLength(ValidationRules.NameMaxLength)
                .WithName("Fornavn");

            RuleFor(v => v.LastName)
                .NotEmpty()
                .MaximumLength(ValidationRules.NameMaxLength)
                .WithName("Etternavn");

            Include(new ContactableValidator());

            RuleFor(v => v.Role)
                .NotEmpty()
                .WithName("Rolle");

            RuleFor(v => v.Password)
                .NotEmpty()
                .MinimumLength(ValidationRules.UserPasswordMinLength)
                .WithName("Passord");
        }
    }
}