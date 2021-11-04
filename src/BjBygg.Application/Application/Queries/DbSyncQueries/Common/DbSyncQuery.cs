using BjBygg.Application.Application.Common.Dto;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public abstract class DbSyncQuery
    {
        public DbSyncQuery(DbSyncQueryPayload _payload)
        {
            Timestamp = _payload.Timestamp;
            InitialSync = _payload.InitialSync;
            User = _payload.User;
        }
        public long? Timestamp { get; set; }
        public bool InitialSync { get; set; }
        public ApplicationUserDto User { get; set; }
    }

    public class DbSyncQueryPayload
    {
        public long? Timestamp { get; set; }
        public bool InitialSync { get; set; }
        public ApplicationUserDto User { get; set; }
    }
}
