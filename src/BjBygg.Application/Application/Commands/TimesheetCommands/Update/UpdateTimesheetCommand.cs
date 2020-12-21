using BjBygg.Application.Common.BaseEntityCommands.Update;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Update
{
    public class UpdateTimesheetCommand : UpdateCommand
    {
        public string MissionId { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public string Comment { get; set; }

    }
}
