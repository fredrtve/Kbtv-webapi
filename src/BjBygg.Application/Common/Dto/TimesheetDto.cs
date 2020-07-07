using CleanArchitecture.Core.Enums;
using System;

namespace BjBygg.Application.Common
{
    public class TimesheetDto : DbSyncDto
    {
        public int Id { get; set; }

        public int MissionId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double TotalHours { get; set; }

        public TimesheetStatus Status { get; set; }

        public string Comment { get; set; }

        public string? UserName { get; set; }
    }
}
