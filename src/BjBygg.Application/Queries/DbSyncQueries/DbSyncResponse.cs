using BjBygg.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public class DbSyncResponse<T> where T : DbSyncDto
    {
        public DbSyncResponse(IEnumerable<T> _entities, IEnumerable<int>? _deletedEntities)
        {
            Entities = _entities;
            DeletedEntities = _deletedEntities;
        }

        public IEnumerable<T> Entities { get; set; }
        public IEnumerable<int>?  DeletedEntities { get; set; }
        public double Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
