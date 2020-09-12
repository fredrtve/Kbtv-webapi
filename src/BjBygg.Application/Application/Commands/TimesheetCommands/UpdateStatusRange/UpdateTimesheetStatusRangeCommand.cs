using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.Core.Enums;
using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatusRange
{
    public class UpdateTimesheetStatusRangeCommand : IRequest
    {
        public IEnumerable<string> Ids { get; set; }
        public TimesheetStatus Status { get; set; }
    }
}
