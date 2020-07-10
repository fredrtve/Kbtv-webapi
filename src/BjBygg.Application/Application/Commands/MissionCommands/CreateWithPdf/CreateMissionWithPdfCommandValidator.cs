using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf
{
    public class CreateMissionWithPdfCommandValidator : AbstractValidator<CreateMissionWithPdfCommand>
    {
        public CreateMissionWithPdfCommandValidator()
        {
            RuleFor(v => v.Files)
               .NotEmpty();
        }
    }
}
