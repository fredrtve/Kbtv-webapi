using BjBygg.Core;
using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionTypeCommands.Create
{
    public class CreateMissionTypeCommandValidator : AbstractValidator<CreateMissionTypeCommand>
    {
        public CreateMissionTypeCommandValidator()
        {
            RuleFor(v => v.Id)
                 .NotEmpty();

            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(ValidationRules.NameMaxLength)
                .WithName("Navn");
        }
    }
}