using BjBygg.Application.Common;
using MediatR;
using System;

namespace BjBygg.Application.Commands.TimesheetCommands.Update
{
    public class UpdateTimesheetCommand : IRequest<TimesheetDto>
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comment { get; set; }

    }
}
