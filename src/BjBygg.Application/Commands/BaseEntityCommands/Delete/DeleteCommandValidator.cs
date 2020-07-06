using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.BaseEntityCommands.Delete
{
    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();
        }
    }
}
