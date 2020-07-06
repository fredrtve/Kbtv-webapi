using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.IdentityCommands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(v => v.UserName)
               .NotEmpty();

            RuleFor(v => v.Password)
                .NotEmpty();
        }
    }
}
