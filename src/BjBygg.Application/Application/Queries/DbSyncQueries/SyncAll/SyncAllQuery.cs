using MediatR;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllQuery : IRequest<SyncAllResponse>
    {
        public int? InitialNumberOfMonths { get; set; }

        public long? Timestamp { get; set; }

    }
}
