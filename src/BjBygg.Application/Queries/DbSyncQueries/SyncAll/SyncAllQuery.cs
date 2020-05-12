using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllQuery: IRequest<SyncAllResponse>
    {
        public double? Timestamp { get; set; }

        public string? UserName { get; set; }

    }
}
