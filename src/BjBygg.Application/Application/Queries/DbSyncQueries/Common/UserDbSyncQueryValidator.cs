using FluentValidation;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public class UserDbSyncQueryValidator : AbstractValidator<UserDbSyncQuery>
    {
        public UserDbSyncQueryValidator()
        {
            RuleFor(v => v.User)
                .NotEmpty()
                .WithName("Bruker");
        }
    }
}
