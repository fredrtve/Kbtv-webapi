using CleanArchitecture.Core;
using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.Notes.Create
{
    public class CreateMissionNoteCommandValidator : AbstractValidator<CreateMissionNoteCommand>
    {
        public CreateMissionNoteCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.MissionId)
               .NotEmpty()
               .WithName("Oppdrag");

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
