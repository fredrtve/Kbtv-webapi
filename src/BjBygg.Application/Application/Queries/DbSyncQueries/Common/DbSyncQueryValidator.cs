using FluentValidation;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public class DbSyncQueryValidator : AbstractValidator<DbSyncQuery>
    {
        public DbSyncQueryValidator()
        {
            RuleFor(v => v.User)
                .NotEmpty()
                .WithName("Bruker");
        }
    }
}
