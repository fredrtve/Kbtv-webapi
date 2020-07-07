using FluentValidation;

namespace BjBygg.Application.Commands.MissionTypeCommands.Create
{
    public class CreateMissionTypeCommandValidator : AbstractValidator<CreateMissionTypeCommand>
    {
        public CreateMissionTypeCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(45);
        }
    }
}
