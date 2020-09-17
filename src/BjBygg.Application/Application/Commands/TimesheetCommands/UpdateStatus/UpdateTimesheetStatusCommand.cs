using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core.Enums;
using MediatR;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatus
{
    public class UpdateTimesheetStatusCommand : IOptimisticCommand
    {
        public string Id { get; set; }
        public TimesheetStatus Status { get; set; }
    }
}
