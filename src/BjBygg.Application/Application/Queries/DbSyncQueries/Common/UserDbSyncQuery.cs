using BjBygg.Application.Identity.Common;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public abstract class UserDbSyncQuery : DbSyncQuery
    {
        public UserDto User { get; set; }
    }
}
