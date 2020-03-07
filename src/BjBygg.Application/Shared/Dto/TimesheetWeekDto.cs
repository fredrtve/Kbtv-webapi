using CleanArchitecture.Core.Enums;
using CleanArchitecture.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Shared
{
    public class TimesheetWeekDto : DbSyncDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Year { get; set; }
        public int WeekNr { get; set; }
        public TimesheetStatus Status { get; set; }
    }
}
