using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommandValidator : AbstractValidator<UpdateMissionCommand>
    {
        public UpdateMissionCommandValidator()
        {
            RuleFor(v => v.Id)
                 .NotEmpty();

            RuleFor(v => v.Address)
                 .NotEmpty()
                 .MaximumLength(100)
                 .WithName("Adresse");

            RuleFor(v => v.PhoneNumber)
                 .MinimumLength(4)
                 .MaximumLength(12)
                 .WithName("Mobilnummer");

            RuleFor(v => v.Description)
                 .MaximumLength(400)
                 .WithName("Beskrivelse");

            RuleFor(v => v.Employer)
               .Must(v => (v == null) || !(string.IsNullOrWhiteSpace(v.Id) || string.IsNullOrWhiteSpace(v.Name)))
               .WithName("Oppdragsgiver");

            RuleFor(v => v.MissionType)
               .Must(v => (v == null) || !(string.IsNullOrWhiteSpace(v.Id) || string.IsNullOrWhiteSpace(v.Name)))
               .WithName("Oppdragstype");
        }
    }
}
