using MediatR;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public abstract class DbSyncQuery
    {
        public long? InitialTimestamp { get; set; }
        public long? Timestamp { get; set; }
    }
}
