using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Entities
{
    public class MissionReport : BaseEntity, IMissionChildEntity
    {
        public MissionReport()
        {
        }

        public Mission Mission { get; set; }
        public int MissionId { get; set; }
        public Uri FileURL { get; set; }
        public ReportType ReportType { get; set; }
        public int ReportTypeId { get; set; }
    }
}
