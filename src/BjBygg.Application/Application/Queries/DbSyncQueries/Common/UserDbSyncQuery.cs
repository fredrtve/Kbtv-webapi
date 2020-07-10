using BjBygg.Application.Identity.Common;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public abstract class UserDbSyncQuery<T> : DbSyncQuery<T> where T : DbSyncDto
    {
        public UserDto User { get; set; }
    }
}
