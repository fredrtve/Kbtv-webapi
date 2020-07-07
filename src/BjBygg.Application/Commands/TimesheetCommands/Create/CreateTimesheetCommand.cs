using BjBygg.Application.Common;
using MediatR;
using System;

namespace BjBygg.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetCommand : IRequest<TimesheetDto>
    {
        public int MissionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comment { get; set; }
    }
}
