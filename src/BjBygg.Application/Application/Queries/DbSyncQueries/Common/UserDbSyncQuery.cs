using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Identity.Common;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public abstract class UserDbSyncQuery : DbSyncQuery
    {
        public ApplicationUserDto User { get; set; }
    }
}
