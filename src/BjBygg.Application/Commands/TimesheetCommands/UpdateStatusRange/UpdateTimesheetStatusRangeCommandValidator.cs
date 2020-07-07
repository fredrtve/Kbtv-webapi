

using FluentValidation;

namespace BjBygg.Application.Commands.TimesheetCommands.UpdateStatusRange
{
    public class UpdateTimesheetStatusRangeCommandValidator : AbstractValidator<UpdateTimesheetStatusRangeCommand>
    {
        public UpdateTimesheetStatusRangeCommandValidator()
        {
            RuleFor(v => v.Ids)
                .NotEmpty();

            RuleFor(v => v.Status)
                .NotEmpty();
        }
    }
}
