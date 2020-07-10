using FluentValidation;

namespace BjBygg.Application.Identity.Queries.UserQueries.UserByUserName
{
    public class UserByUserNameQueryValidator : AbstractValidator<UserByUserNameQuery>
    {
        public UserByUserNameQueryValidator()
        {
            RuleFor(v => v.UserName)
                .NotEmpty();
        }
    }
}
