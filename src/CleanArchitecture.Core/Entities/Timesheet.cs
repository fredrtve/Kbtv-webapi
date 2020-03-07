using CleanArchitecture.Core.Enums;
using CleanArchitecture.SharedKernel;
using System;

namespace CleanArchitecture.Core.Entities
{
    public class Timesheet : BaseEntity
    {
        public string UserName { get; set; }

        public int TimesheetWeekId { get; set; }

        public TimesheetWeek TimesheetWeek { get; set; }

        public int MissionId { get; set; }

        public Mission Mission { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimesheetStatus Status { get; set; }

        public double GetTotalHours()
        {
            return (EndTime - StartTime).TotalHours;
        }
    }
}
