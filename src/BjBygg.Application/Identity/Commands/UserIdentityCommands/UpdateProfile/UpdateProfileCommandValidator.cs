using FluentValidation;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(v => v.PhoneNumber)
                .MaximumLength(12);

            RuleFor(v => v.Email)
                .EmailAddress();
        }
    }
}
