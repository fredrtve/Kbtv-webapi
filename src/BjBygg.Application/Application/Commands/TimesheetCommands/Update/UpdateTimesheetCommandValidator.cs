using FluentValidation;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Update
{
    public class UpdateTimesheetCommandValidator : AbstractValidator<UpdateTimesheetCommand>
    {
        public UpdateTimesheetCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.Comment)
                .MaximumLength(400)
                .WithName("Kommentar");
        }
    }
}
