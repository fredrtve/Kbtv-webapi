using FluentValidation;

namespace BjBygg.Application.Common.BaseEntityCommands.DeleteRange
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
