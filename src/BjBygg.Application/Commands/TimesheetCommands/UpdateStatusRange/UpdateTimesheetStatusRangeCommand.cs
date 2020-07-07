using BjBygg.Application.Common;
using CleanArchitecture.Core.Enums;
using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Commands.TimesheetCommands.UpdateStatusRange
{
    public class UpdateTimesheetStatusRangeCommand : IRequest<IEnumerable<TimesheetDto>>
    {
        public IEnumerable<int> Ids { get; set; }
        public TimesheetStatus Status { get; set; }
    }
}
