using FluentValidation;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Create
{
    public class CreateDocumentTypeCommandValidator : AbstractValidator<CreateDocumentTypeCommand>
    {
        public CreateDocumentTypeCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(45);
        }
    }
}
