using FluentValidation;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetCommandValidator : AbstractValidator<CreateTimesheetCommand>
    {
        public CreateTimesheetCommandValidator()
        {
            RuleFor(v => v.Id)
                 .NotEmpty();

            RuleFor(v => v.MissionId)
                .NotEmpty();

            RuleFor(v => v.StartTime)
                .NotEmpty()
                .LessThan(x => x.EndTime);

            RuleFor(v => v.EndTime)
                .NotEmpty()
                .GreaterThan(x => x.StartTime);

            RuleFor(v => v.Comment)
                .NotEmpty()
                .MaximumLength(400);
        }
    }
}
