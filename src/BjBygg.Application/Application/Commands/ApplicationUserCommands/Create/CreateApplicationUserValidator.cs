using BjBygg.Application.Application.Commands.UserCommands.Create;
using BjBygg.Application.Common;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Application.Commands.ApplicationUserCommands
{
    public class CreateApplicationUserValidator : AbstractValidator<CreateApplicationUserCommand>
    {
        public CreateApplicationUserValidator()
        {
            RuleFor(v => v.EmployerId)
                .NotEmpty()
                .When(x => x.Role == Roles.Employer)
                .WithName("Oppdragsgiver");

        }
    }
}
