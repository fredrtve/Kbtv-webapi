using BjBygg.Application.Application.Common;
using CleanArchitecture.Core;
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

            RuleFor(v => v.File)
                .NotEmpty()
                .SetValidator(new BasicFileStreamValidator(ValidationRules.DocumentFileExtensions))
                .WithName("Filer");

            RuleFor(v => v.MissionId)
                .NotEmpty()
                .WithName("Oppdrag");
        }
    }
}
