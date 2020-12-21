using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Identity.Common;
using System;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllResponse
    {
        public double Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds() * 1000;
        public SyncArraysResponse Arrays { get; set; }
        public SyncValuesResponse Values { get; set; }

    }
}
