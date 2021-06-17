using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Application.Common.Dto
{
    public class UserTimesheetDto : DbSyncDto
    {
        public string Id { get; set; }

        public string MissionId { get; set; }

        public long StartTime { get; set; }

        public long EndTime { get; set; }

        public double TotalHours { get; set; }

        public TimesheetStatus Status { get; set; }

        public string Comment { get; set; }
    }
}
