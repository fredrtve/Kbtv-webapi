using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetCommand : IRequest<TimesheetDto>
    {
        public string UserName { get; set; }

        [Required]
        public int MissionId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(400, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string Comment { get; set; }
    }
}
