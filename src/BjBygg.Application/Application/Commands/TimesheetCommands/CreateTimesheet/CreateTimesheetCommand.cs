using BjBygg.Application.Common.BaseEntityCommands.Create;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.CreateTimesheet
{
    public class CreateTimesheetCommand : CreateCommand
    {
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public string Comment { get; set; }
        public string? UserName { get; set; }
        public string MissionActivityId { get; set; }
    }
}
