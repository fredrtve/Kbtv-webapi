using BjBygg.Application.Common;
using FluentValidation;

namespace BjBygg.Application.Queries.DbSyncQueries.Common
{
    public class UserDbSyncQueryValidator : AbstractValidator<UserDbSyncQuery<DbSyncDto>>
    {
        public UserDbSyncQueryValidator()
        {
            RuleFor(v => v.User)
                .NotEmpty();
        }
    }
}
