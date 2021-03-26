using BjBygg.Application.Application.Common.Dto;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public abstract class UserDbSyncQuery : DbSyncQuery
    {
        public ApplicationUserDto User { get; set; }
    }
}
