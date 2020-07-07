using FluentValidation;

namespace BjBygg.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommandValidator : AbstractValidator<UploadMissionImageCommand>
    {
        public UploadMissionImageCommandValidator()
        {
            RuleFor(v => v.Files)
               .NotEmpty();

            RuleFor(v => v.MissionId)
               .NotEmpty();
        }
    }
}
