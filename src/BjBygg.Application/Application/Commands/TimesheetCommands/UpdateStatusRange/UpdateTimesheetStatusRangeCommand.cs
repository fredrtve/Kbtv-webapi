using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.Core.Enums;
using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatusRange
{
    public class UpdateTimesheetStatusRangeCommand : IRequest<List<TimesheetDto>>
    {
        public IEnumerable<int> Ids { get; set; }
        public TimesheetStatus Status { get; set; }
    }
}
