using FluentValidation;

namespace BjBygg.Application.Commands.TimesheetCommands.UpdateStatus
{
    public class UpdateTimesheetStatusCommandValidator : AbstractValidator<UpdateTimesheetStatusCommand>
    {
        public UpdateTimesheetStatusCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.Status)
                .NotEmpty();
        }
    }
}
