using BjBygg.Application.Common;

namespace BjBygg.Application.Queries.DbSyncQueries.Common
{
    public abstract class UserDbSyncQuery<T> : DbSyncQuery<T> where T : DbSyncDto
    {
        public UserDto User { get; set; }
    }
}
