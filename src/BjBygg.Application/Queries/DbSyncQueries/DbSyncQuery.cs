using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public abstract class DbSyncQuery<T> : IRequest<DbSyncResponse<T>> where T : DbSyncDto
    {
        public double? Timestamp { get; set; }
        public UserDto? User { get; set; }
    }
}
