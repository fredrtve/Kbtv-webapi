using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Shared
{
    public class MissionReportDto : DbSyncDto
    {
        public int Id { get; set; }

        public int MissionId { get; set; }

        public Uri FileURL { get; set; }

        public int MissionReportTypeId { get; set; }

        public MissionReportTypeDto MissionReportType { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
