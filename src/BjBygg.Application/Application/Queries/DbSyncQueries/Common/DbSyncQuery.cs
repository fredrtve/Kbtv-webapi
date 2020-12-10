using MediatR;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public abstract class DbSyncQuery<T> : IRequest<DbSyncArrayResponse<T>> where T : DbSyncDto
    {
        public int? InitialNumberOfMonths { get; set; }
        public long? Timestamp { get; set; }
    }
}
