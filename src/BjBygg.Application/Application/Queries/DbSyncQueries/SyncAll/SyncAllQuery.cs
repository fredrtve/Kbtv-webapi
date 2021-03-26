using MediatR;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllQuery : IRequest<SyncAllResponse>
    {
        public long? Timestamp { get; set; }

        public bool InitialSync { get; set; }
    }
}
