using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.TimesheetCommands.UpdateStatusRange
{
    public class UpdateTimesheetStatusRangeCommand : IRequest<IEnumerable<TimesheetDto>>
    {   
        public string UserName { get; set; }
        public string Role { get; set; }

        [Required]
        public IEnumerable<int> Ids { get; set; }
        [Required]
        public TimesheetStatus Status { get; set; }
    }
}
