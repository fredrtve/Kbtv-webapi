using BjBygg.Application.Common;
using MediatR;

namespace BjBygg.Application.Queries.DbSyncQueries.Common
{
    public abstract class DbSyncQuery<T> : IRequest<DbSyncResponse<T>> where T : DbSyncDto
    {
        public int? InitialNumberOfMonths { get; set; }
        public double? Timestamp { get; set; }
    }
}
