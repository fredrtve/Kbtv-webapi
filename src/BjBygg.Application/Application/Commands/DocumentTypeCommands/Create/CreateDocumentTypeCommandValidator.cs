using CleanArchitecture.Core;
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
                .MaximumLength(ValidationRules.NameMaxLength)
                .WithName("Navn"); 
        }
    }
}
