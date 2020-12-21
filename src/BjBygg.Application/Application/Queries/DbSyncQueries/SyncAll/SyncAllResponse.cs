using System;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll
{
    public class SyncAllResponse
    {
        public double Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds() * 1000;
        public SyncArraysResponse Arrays { get; set; }
        public SyncValuesResponse Values { get; set; }

    }
}
