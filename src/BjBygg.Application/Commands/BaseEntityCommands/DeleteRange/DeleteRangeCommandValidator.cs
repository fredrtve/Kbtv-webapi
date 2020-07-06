using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.BaseEntityCommands.DeleteRange
{
    public class DeleteRangeCommandValidator : AbstractValidator<DeleteRangeCommand>
    {
        public DeleteRangeCommandValidator()
        {
            RuleFor(v => v.Ids)
                .NotEmpty();
        }
    }
}
