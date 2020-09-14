using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using System;

namespace BjBygg.Application.Application.Common.Dto
{
    public class MissionImageDto : DbSyncDto
    {
        public string Id { get; set; }

        public string MissionId { get; set; }

        public Uri FileName { get; set; }

    }
}
