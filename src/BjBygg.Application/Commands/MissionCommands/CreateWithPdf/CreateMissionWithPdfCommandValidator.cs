using FluentValidation;

namespace BjBygg.Application.Commands.MissionCommands.CreateWithPdf
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
