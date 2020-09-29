using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserCommands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .MaximumLength(45)
                .WithName("Brukernavn");

            RuleFor(v => v.FirstName)
                .MaximumLength(45)
                .WithName("Fornavn");

            RuleFor(v => v.LastName)
                .MaximumLength(45)
                .WithName("Etternavn");

            RuleFor(v => v.PhoneNumber)
                .MaximumLength(12)
                .WithName("Mobilnummer");

            RuleFor(v => v.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithName("Epost");
        }
    }
}
