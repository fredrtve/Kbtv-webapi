using CleanArchitecture.Core.Enums;
using CleanArchitecture.SharedKernel;
using System.Collections.Generic;


namespace CleanArchitecture.Core.Entities
{
    public class TimesheetWeek : BaseEntity
    {
        public string UserName { get; set; }

        public int Year { get; set; }

        public int WeekNr { get; set; }
        public TimesheetStatus Status { get; set; }

        public IEnumerable<Timesheet> Timesheets { get; set; }
    }
}
