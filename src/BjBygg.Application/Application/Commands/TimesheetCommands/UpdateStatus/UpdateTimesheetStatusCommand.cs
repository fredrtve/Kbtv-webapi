using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.Core.Enums;
using MediatR;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatus
{
    public class UpdateTimesheetStatusCommand : IRequest
    {
        public string Id { get; set; }
        public TimesheetStatus Status { get; set; }
    }
}
