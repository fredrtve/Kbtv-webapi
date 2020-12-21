using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core.Enums;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatusRange
{
    public class UpdateTimesheetStatusRangeCommand : IOptimisticCommand
    {
        public IEnumerable<string> Ids { get; set; }
        public TimesheetStatus Status { get; set; }
    }
}
