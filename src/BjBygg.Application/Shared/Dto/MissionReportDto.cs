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

        public int ReportTypeId { get; set; }

        public ReportTypeDto ReportType { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
