using BjBygg.Core.Enums;
using BjBygg.SharedKernel;
using System;

namespace BjBygg.Core.Entities
{
    public class Timesheet : UserEntity
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double TotalHours { get; set; }

        public TimesheetStatus Status { get; set; }

        public string Comment { get; set; }
        public MissionActivity MissionActivity { get; set; }
        public string MissionActivityId { get; set; }

    }
}
