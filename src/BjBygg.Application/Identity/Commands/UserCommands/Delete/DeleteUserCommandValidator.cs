using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserCommands.Delete
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty()
                .WithName("Brukernavn");
        }
    }
}
