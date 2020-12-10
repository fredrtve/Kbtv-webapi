using System;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public class DbSyncArrayResponse<T> where T : DbSyncDto
    {
        public DbSyncArrayResponse(IEnumerable<T> _entities, IEnumerable<string>? _deletedEntities)
        {
            Entities = _entities;
            DeletedEntities = _deletedEntities;
        }

        public IEnumerable<T> Entities { get; set; }
        public IEnumerable<string>? DeletedEntities { get; set; }

    }
}
