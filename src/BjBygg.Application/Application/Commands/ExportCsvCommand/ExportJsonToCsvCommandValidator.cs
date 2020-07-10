using FluentValidation;

namespace BjBygg.Application.Application.Commands.ExportCsvCommand
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
