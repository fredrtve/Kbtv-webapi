using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Enums;

namespace BjBygg.Application.Application.Common.Dto
{
    public class TimesheetDto : DbSyncDto
    {
        public string Id { get; set; }

        public string MissionId { get; set; }

        public long StartTime { get; set; }

        public long EndTime { get; set; }

        public double TotalHours { get; set; }

        public TimesheetStatus Status { get; set; }

        public string Comment { get; set; }

        public string? UserName { get; set; }
    }
}
