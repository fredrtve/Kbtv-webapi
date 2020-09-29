using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserCommands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .MaximumLength(45)
                .WithName("Brukernavn");

            RuleFor(v => v.FirstName)
                .NotEmpty()
                .MaximumLength(45)
                .WithName("Fornavn");

            RuleFor(v => v.LastName)
                .NotEmpty()
                .MaximumLength(45)
                .WithName("Etternavn");

            RuleFor(v => v.PhoneNumber)
                .MaximumLength(12)
                .WithName("Mobilnummer");

            RuleFor(v => v.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithName("Epost");

            RuleFor(v => v.Role)
                .NotEmpty()
                .WithName("Rolle");

            RuleFor(v => v.Password)
                .NotEmpty()
                .MinimumLength(7)
                .WithName("Passord");
        }
    }
}