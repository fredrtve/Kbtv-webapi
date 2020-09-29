using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommandValidator : AbstractValidator<UploadMissionDocumentCommand>
    {
        public UploadMissionDocumentCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.DocumentType)
               .Must(v => (v == null) || !(string.IsNullOrWhiteSpace(v.Id) || string.IsNullOrWhiteSpace(v.Name)))
               .WithName("Dokumenttype");

            RuleFor(v => v.DocumentTypeId)
               .NotEmpty().When(v => v.DocumentType == null || string.IsNullOrEmpty(v.DocumentType.Id))
               .WithName("Dokumenttype");

            RuleFor(v => v.File)
                .NotEmpty()
                .WithName("Filer");

            RuleFor(v => v.MissionId)
                .NotEmpty()
                .WithName("Oppdrag");
        }
    }
}
