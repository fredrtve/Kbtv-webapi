using FluentValidation;

namespace BjBygg.Application.Commands.MissionTypeCommands.Update
{
    public class UpdateMissionTypeCommandValidator : AbstractValidator<UpdateMissionTypeCommand>
    {
        public UpdateMissionTypeCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(45);
        }
    }
}
