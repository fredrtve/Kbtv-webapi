using BjBygg.Application.Common;
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
        [Required]
        public IEnumerable<int> Ids { get; set; }
        [Required]
        public TimesheetStatus Status { get; set; }
    }
}
