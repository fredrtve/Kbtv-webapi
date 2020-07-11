using BjBygg.Application.Application.Common.Dto;
using MediatR;
using System;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetCommand : IRequest<TimesheetDto>
    {
        public int MissionId { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public string Comment { get; set; }
    }
}
