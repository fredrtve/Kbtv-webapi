using FluentValidation;

namespace BjBygg.Application.Commands.BaseEntityCommands.MailEntitiesCommand
{
    public class MailEntitiesCommandValidator : AbstractValidator<MailEntitiesCommand>
    {
        public MailEntitiesCommandValidator()
        {
            RuleFor(v => v.ToEmail)
               .NotEmpty()
               .EmailAddress();

            RuleFor(v => v.Ids)
               .NotEmpty();
        }
    }
}
