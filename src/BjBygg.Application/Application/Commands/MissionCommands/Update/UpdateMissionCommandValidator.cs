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
                 .MaximumLength(100);

            RuleFor(v => v.PhoneNumber)
                 .MinimumLength(4)
                 .MaximumLength(12);

            RuleFor(v => v.Description)
                 .MaximumLength(400);

            RuleFor(v => v.Employer)
               .Must(v => (v == null) || !(string.IsNullOrWhiteSpace(v.Id) || string.IsNullOrWhiteSpace(v.Name)));

            RuleFor(v => v.MissionType)
               .Must(v => (v == null) || !(string.IsNullOrWhiteSpace(v.Id) || string.IsNullOrWhiteSpace(v.Name)));
        }
    }
}
