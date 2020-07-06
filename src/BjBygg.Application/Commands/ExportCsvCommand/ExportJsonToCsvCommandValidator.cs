using FluentValidation;

namespace BjBygg.Application.Commands.ExportCsvCommands
{
    public class ExportJsonToCsvCommandValidator : AbstractValidator<ExportJsonToCsvCommand>
    {
        public ExportJsonToCsvCommandValidator()
        {
            RuleFor(v => v.PropertyMap)
               .NotEmpty();

            RuleFor(v => v.JsonObjects)
                .NotEmpty();
        }
    }
}
