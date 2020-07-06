using BjBygg.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public abstract class DbSyncQuery<T> : IRequest<DbSyncResponse<T>> where T : DbSyncDto
    {
        public int? InitialNumberOfMonths { get; set; }
        public double? Timestamp { get; set; }
    }
}
