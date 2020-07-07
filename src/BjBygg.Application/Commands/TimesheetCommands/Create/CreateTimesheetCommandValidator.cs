using FluentValidation;

namespace BjBygg.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetCommandValidator : AbstractValidator<CreateTimesheetCommand>
    {
        public CreateTimesheetCommandValidator()
        {
            RuleFor(v => v.MissionId)
                .NotEmpty();

            RuleFor(v => v.StartTime)
                .NotEmpty();

            RuleFor(v => v.EndTime)
                .NotEmpty();

            RuleFor(v => v.Comment)
                .NotEmpty()
                .MaximumLength(400);
        }
    }
}
