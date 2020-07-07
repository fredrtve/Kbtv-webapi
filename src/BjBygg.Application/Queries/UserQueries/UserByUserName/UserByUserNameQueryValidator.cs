using FluentValidation;

namespace BjBygg.Application.Queries.UserQueries.UserByUserName
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
