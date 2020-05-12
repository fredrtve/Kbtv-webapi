using BjBygg.Application.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public class DbSyncResponse<T> where T : DbSyncDto
    {
        public IEnumerable<T> Entities { get; set; }
        public IEnumerable<int>?  DeletedEntities { get; set; }
        public double Timestamp { get; set; }
    }
}
