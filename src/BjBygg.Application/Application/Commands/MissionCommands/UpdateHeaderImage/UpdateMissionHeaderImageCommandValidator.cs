using BjBygg.Application.Commands.MissionCommands.UpdateHeaderImage;
using BjBygg.Core;
using FluentValidation;

namespace BjBygg.Application.Application.Commands.MissionCommands.UpdateHeaderImage
{
    public class UpdateMissionHeaderImageCommandValidator : AbstractValidator<UpdateMissionHeaderImageCommand>
    {
        public UpdateMissionHeaderImageCommandValidator()
        {
            RuleFor(v => v.Id)
                 .NotEmpty();

            RuleFor(x => x.FileExtension)
                         .Must(x => ValidationRules.ImageFileExtensions.Contains(x?.ToLower()))
                         .WithName("Filtype");

            RuleFor(v => v.Image)
                .NotEmpty()
                .WithName("Bilde");
        }
    }
}
