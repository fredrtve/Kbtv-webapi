using CleanArchitecture.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class ReportType : BaseEntity
    {
        public ReportType() { }
        public string Name { get; set; }
        public List<MissionReport> MissionReports { get; set; }
    }
}
