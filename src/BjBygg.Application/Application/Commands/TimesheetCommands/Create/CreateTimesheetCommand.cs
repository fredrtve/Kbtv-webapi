using BjBygg.Application.Common.BaseEntityCommands.Create;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetCommand : CreateCommand
    {
        public string MissionId { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public string Comment { get; set; }
    }
}
