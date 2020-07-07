using FluentValidation;

namespace BjBygg.Application.Commands.UserCommands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .MaximumLength(45);

            RuleFor(v => v.FirstName)
                .MaximumLength(45);

            RuleFor(v => v.LastName)
                .MaximumLength(45);

            RuleFor(v => v.PhoneNumber)
                .MaximumLength(12);

            RuleFor(v => v.Email)
                .EmailAddress();

            RuleFor(v => v.Role)
                .NotEmpty();
        }
    }
}
