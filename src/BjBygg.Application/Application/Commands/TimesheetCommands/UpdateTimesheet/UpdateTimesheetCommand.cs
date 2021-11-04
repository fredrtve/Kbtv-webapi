using BjBygg.Application.Common.BaseEntityCommands.Update;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateTimesheet
{
    public class UpdateTimesheetCommand : UpdateCommand
    {
        public string MissionActivityId { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public string Comment { get; set; }

    }
}
