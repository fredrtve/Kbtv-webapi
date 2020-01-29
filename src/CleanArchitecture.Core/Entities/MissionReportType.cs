using CleanArchitecture.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class MissionReportType : BaseEntity
    {
        public MissionReportType() { }
        public string Name { get; set; }
        public List<MissionReport> MissionReports { get; set; }
    }
}
