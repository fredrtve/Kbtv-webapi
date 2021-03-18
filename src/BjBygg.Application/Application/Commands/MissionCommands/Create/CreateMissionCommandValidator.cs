using BjBygg.Application.Common.Validation;
using BjBygg.Core;
using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.Create
{
    public class CreateMissionCommandValidator : AbstractValidator<CreateMissionCommand>
    {
        public CreateMissionCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.Address)
                .NotEmpty()
                .MaximumLength(ValidationRules.AddressMaxLength)
                .WithName("Adresse");

            Include(new ContactableValidator());

            RuleFor(v => v.Description)
                 .MaximumLength(ValidationRules.MissionDescriptionMaxLength)
                 .When(x => !string.IsNullOrWhiteSpace(x.Description))
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