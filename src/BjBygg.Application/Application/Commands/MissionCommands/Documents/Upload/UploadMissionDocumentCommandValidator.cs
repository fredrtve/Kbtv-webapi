using BjBygg.Application.Application.Common;
using BjBygg.Core;
using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommandValidator : AbstractValidator<UploadMissionDocumentCommand>
    {
        public UploadMissionDocumentCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.Name)
                 .NotEmpty()
                 .MaximumLength(ValidationRules.NameMaxLength)
                 .WithName("Navn");

            RuleFor(x => x.FileExtension)
                         .Must(x => ValidationRules.DocumentFileExtensions.Contains(x?.ToLower()))
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
