using BjBygg.Application.Common;
using BjBygg.Application.Common.Validation;
using BjBygg.Core;
using FluentValidation;

namespace BjBygg.Application.Application.Commands.ApplicationUserCommands.Update
{
    public class UpdateApplicationUserCommandValidator : AbstractValidator<UpdateApplicationUserCommand>
    {
        public UpdateApplicationUserCommandValidator()
        {
            RuleFor(v => v.EmployerId)
               .NotEmpty()
               .When(x => x.Role == Roles.Employer)
               .WithName("Oppdragsgiver");
        }
    }
}
