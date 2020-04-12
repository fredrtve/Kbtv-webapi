using CleanArchitecture.Core.Enums;
using CleanArchitecture.SharedKernel;
using System;

namespace CleanArchitecture.Core.Entities
{
    public class Timesheet : UserEntity
    {
        public int MissionId { get; set; }

        public Mission Mission { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double TotalHours { get; set; }

        public TimesheetStatus Status { get; set; }

    }
}
