using BjBygg.Application.Application.Common;
using BjBygg.Core;
using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommandValidator : AbstractValidator<UploadMissionImageCommand>
    {
        public UploadMissionImageCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(x => x.FileExtension)
                .Must(x => ValidationRules.ImageFileExtensions.Contains(x?.ToLower()))
                .WithName("Filtype");

            RuleFor(v => v.File)
                .NotEmpty()
                .WithName("File");

            RuleFor(v => v.MissionId)
                .NotEmpty()
                .WithName("Oppdrag");
        }
    }
}
