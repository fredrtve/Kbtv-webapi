using System.Collections.Generic;
using System.Linq;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Common
{
    public class SyncEntityResponse<T>
    {
        public SyncEntityResponse(IEnumerable<T> _entities, IEnumerable<string> _deletedEntities = null)
        {
            Entities = _entities;
            DeletedEntities = _deletedEntities;
        }

        public IEnumerable<T> Entities { get; set; }
        public IEnumerable<string>? DeletedEntities { get; set; }

    }
}
