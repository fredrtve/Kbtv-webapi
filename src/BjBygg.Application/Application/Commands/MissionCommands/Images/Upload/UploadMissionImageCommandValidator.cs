using BjBygg.Application.Application.Common;
using CleanArchitecture.Core;
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
                .SetValidator(new BasicFileStreamValidator(ValidationRules.ImageFileExtensions))
                .WithName("Filer");

            RuleFor(v => v.MissionId)
                .NotEmpty()
                .WithName("Oppdrag");
        }
    }
}
