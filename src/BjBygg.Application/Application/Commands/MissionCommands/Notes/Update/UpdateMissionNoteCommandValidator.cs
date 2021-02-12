using CleanArchitecture.Core;
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
                .MaximumLength(ValidationRules.MissionNoteTitleMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Title))
                .WithName("Tittel");

            RuleFor(v => v.Content)
                .NotEmpty()
                .MaximumLength(ValidationRules.MissionNoteContentMaxLength)
                .WithName("Innhold");
        }
    }
}
