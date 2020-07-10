using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.Core.Enums;
using MediatR;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatus
{
    public class UpdateTimesheetStatusCommand : IRequest<TimesheetDto>
    {
        public int Id { get; set; }
        public TimesheetStatus Status { get; set; }
    }
}
