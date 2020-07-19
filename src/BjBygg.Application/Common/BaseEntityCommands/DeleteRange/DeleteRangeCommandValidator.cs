using FluentValidation;

namespace BjBygg.Application.Common.BaseEntityCommands.DeleteRange
{
    public class DeleteRangeCommandValidator<T> : AbstractValidator<T> where T : DeleteRangeCommand
    {
        public DeleteRangeCommandValidator()
        {
            RuleFor(v => v.Ids)
                .NotEmpty();
        }
    }
}
