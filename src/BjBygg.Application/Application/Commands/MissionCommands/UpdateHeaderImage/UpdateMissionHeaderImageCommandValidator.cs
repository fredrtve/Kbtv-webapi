using BjBygg.Application.Application.Common;
using CleanArchitecture.Core;
using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.UpdateHeaderImage
{
    public class UpdateMissionHeaderImageCommandValidator : AbstractValidator<UpdateMissionHeaderImageCommand>
    {
        public UpdateMissionHeaderImageCommandValidator()
        {
            RuleFor(v => v.Id)
                 .NotEmpty();

            RuleFor(v => v.Image)
                 .NotEmpty()
                 .SetValidator(new BasicFileStreamValidator(ValidationRules.ImageFileExtensions))
                 .WithName("Bilde");
        }
    }
}
