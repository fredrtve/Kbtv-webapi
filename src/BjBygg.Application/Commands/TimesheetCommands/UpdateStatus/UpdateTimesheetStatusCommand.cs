using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.TimesheetCommands.UpdateStatus
{
    public class UpdateTimesheetStatusCommand : IRequest<TimesheetDto>
    {   
        public string UserName { get; set; }
        public string Role { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public TimesheetStatus Status { get; set; }
    }
}
