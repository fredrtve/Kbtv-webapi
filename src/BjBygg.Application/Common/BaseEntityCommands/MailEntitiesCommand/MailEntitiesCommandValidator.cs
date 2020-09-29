using FluentValidation;

namespace BjBygg.Application.Common.BaseEntityCommands.MailEntitiesCommand
{
    public class MailEntitiesCommandValidator<T> : AbstractValidator<T> where T : MailEntitiesCommand
    {
        public MailEntitiesCommandValidator()
        {
            RuleFor(v => v.ToEmail)
               .NotEmpty()
               .EmailAddress()
               .WithName("Epost");

            RuleFor(v => v.Ids)
               .NotEmpty();
        }
    }
}
