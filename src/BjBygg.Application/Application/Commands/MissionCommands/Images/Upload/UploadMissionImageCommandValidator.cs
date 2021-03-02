using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommandValidator : AbstractValidator<UploadMissionImageCommand>
    {
        public UploadMissionImageCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.File)
                .NotEmpty()
                .WithName("Filer");

            RuleFor(v => v.MissionId)
                .NotEmpty()
                .WithName("Oppdrag");
        }
    }
}
