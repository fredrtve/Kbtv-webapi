using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Update
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
