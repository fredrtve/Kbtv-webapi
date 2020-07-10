using FluentValidation;

namespace BjBygg.Application.Common.BaseEntityCommands.Delete
{
    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();
        }
    }
}
