using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserCommands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .MaximumLength(45);

            RuleFor(v => v.FirstName)
                .NotEmpty()
                .MaximumLength(45);

            RuleFor(v => v.LastName)
                .NotEmpty()
                .MaximumLength(45);

            RuleFor(v => v.PhoneNumber)
                .NotEmpty()
                .MaximumLength(12);

            RuleFor(v => v.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(v => v.Role)
                .NotEmpty();

            RuleFor(v => v.Password)
                .NotEmpty()
                .MinimumLength(7);
        }
    }
}