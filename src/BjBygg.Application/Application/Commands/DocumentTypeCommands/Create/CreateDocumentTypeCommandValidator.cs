using FluentValidation;

namespace BjBygg.Application.Application.Commands.DocumentTypeCommands.Create
{
    public class CreateDocumentTypeCommandValidator : AbstractValidator<CreateDocumentTypeCommand>
    {
        public CreateDocumentTypeCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(45)
                .WithName("Navn"); 
        }
    }
}
