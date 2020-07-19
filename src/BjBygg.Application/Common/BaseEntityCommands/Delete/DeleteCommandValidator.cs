using FluentValidation;

namespace BjBygg.Application.Common.BaseEntityCommands.Delete
{
    public class DeleteCommandValidator<T> : AbstractValidator<T> where T : DeleteCommand
    {
        public DeleteCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();
        }
    }
}
