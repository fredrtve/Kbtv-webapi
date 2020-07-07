using System;

namespace BjBygg.Application.Common
{
    public class MissionImageDto : DbSyncDto
    {
        public int Id { get; set; }

        public int MissionId { get; set; }

        public Uri FileURL { get; set; }

    }
}
