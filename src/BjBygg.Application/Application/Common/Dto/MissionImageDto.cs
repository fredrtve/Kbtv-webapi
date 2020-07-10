using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using System;

namespace BjBygg.Application.Application.Common.Dto
{
    public class MissionImageDto : DbSyncDto
    {
        public int Id { get; set; }

        public int MissionId { get; set; }

        public Uri FileURL { get; set; }

    }
}
