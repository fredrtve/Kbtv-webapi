using FluentValidation;

namespace BjBygg.Application.Application.Commands.DocumentTypeCommands.Update
{
    public class UpdateDocumentTypeCommandValidator : AbstractValidator<UpdateDocumentTypeCommand>
    {
        public UpdateDocumentTypeCommandValidator()
        {
            RuleFor(v => v.Id)
               .NotEmpty();

            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(45);
        }
    }
}
