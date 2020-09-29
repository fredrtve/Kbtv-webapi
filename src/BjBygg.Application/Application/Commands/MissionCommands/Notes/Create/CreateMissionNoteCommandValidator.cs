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
                .MaximumLength(100)
                .WithName("Tittel");

            RuleFor(v => v.Content)
                .NotEmpty()
                .MaximumLength(400)
                .WithName("Innhold");
        }
    }
}
