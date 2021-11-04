using BjBygg.Core.Enums;
using System;

namespace BjBygg.Application.Application.Queries.TimesheetQueries
{
    public class MissionActivityDto
    {
        public string Id { get; set; }
        public string MissionId { get; set; }
        public string ActivityId { get; set; }
    }

    public abstract class CommmonTimesheetDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string MissionActivityId { get; set; }
        public double TotalHours { get; set; }
        public TimesheetStatus Status { get; set; }
        public string Comment { get; set; }
    } 
    public class TimesheetQueryDto : CommmonTimesheetDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public MissionActivityDto MissionActivity { get; set; }
    }
    public class TimesheetDto : CommmonTimesheetDto
    {
        public long StartTime { get; set; }
        public long EndTime { get; set; }
    }
}
