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
               .NotEmpty().When(v => string.IsNullOrEmpty(v.DocumentTypeId));
            
            RuleFor(v => v.DocumentTypeId)
               .NotEmpty().When(v => v.DocumentType == null || string.IsNullOrEmpty(v.DocumentType.Id));

            RuleFor(v => v.File)
                .NotEmpty();

            RuleFor(v => v.MissionId)
                .NotEmpty();
        }
    }
}
