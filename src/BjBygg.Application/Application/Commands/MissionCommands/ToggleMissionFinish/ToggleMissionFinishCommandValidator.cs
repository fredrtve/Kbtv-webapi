using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.ToggleMissionFinish
{
    public class ToggleMissionFinishCommandValidator : AbstractValidator<ToggleMissionFinishCommand>
    {
        public ToggleMissionFinishCommandValidator()
        {
            RuleFor(v => v.Id)
               .NotEmpty();
        }
    }
}
