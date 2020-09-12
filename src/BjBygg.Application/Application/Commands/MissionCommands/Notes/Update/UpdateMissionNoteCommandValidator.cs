using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.Notes.Update
{
    public class UpdateMissionNoteCommandValidator : AbstractValidator<UpdateMissionNoteCommand>
    {
        public UpdateMissionNoteCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.Title)
                .MaximumLength(100);

            RuleFor(v => v.Content)
                .NotEmpty()
                .MaximumLength(400);
        }
    }
}
