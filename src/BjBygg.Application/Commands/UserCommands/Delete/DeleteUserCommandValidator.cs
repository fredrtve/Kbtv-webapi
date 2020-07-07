using FluentValidation;

namespace BjBygg.Application.Commands.UserCommands.Delete
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty();
        }
    }
}
