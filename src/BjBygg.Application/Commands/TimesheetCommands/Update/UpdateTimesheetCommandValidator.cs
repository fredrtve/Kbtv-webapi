using FluentValidation;

namespace BjBygg.Application.Commands.TimesheetCommands.Update
{
    public class UpdateTimesheetCommandValidator : AbstractValidator<UpdateTimesheetCommand>
    {
        public UpdateTimesheetCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.Comment)
                .MaximumLength(400);
        }
    }
}
