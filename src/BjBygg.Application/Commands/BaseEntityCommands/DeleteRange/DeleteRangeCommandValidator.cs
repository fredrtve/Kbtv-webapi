using FluentValidation;

namespace BjBygg.Application.Commands.BaseEntityCommands.DeleteRange
{
    public class DeleteRangeCommandValidator : AbstractValidator<DeleteRangeCommand>
    {
        public DeleteRangeCommandValidator()
        {
            RuleFor(v => v.Ids)
                .NotEmpty();
        }
    }
}
