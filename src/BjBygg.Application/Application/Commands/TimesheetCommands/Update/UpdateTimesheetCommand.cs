using BjBygg.Application.Application.Common.Dto;
using MediatR;
using System;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Update
{
    public class UpdateTimesheetCommand : IRequest<TimesheetDto>
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comment { get; set; }

    }
}
