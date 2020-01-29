using CleanArchitecture.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class MissionReport : BaseEntity
    {
        public MissionReport()
        {
        }

        public Mission Mission { get; set; }
        public int MissionId { get; set; }
        public Uri FileURL { get; set; }
        public MissionReportType MissionReportType { get; set; }
        public int MissionReportTypeId { get; set; }
    }
}
