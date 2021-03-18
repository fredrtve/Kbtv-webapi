using BjBygg.Core;
using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionTypeCommands.Update
{
    public class UpdateMissionTypeCommandValidator : AbstractValidator<UpdateMissionTypeCommand>
    {
        public UpdateMissionTypeCommandValidator()
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
