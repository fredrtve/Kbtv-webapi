using BjBygg.Application.Queries.MissionQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.TimesheetCommands.Update
{
    public class UpdateTimesheetCommand : IRequest<TimesheetDto>
    {
        [Required]
        public int Id { get; set; }

        public string UserName { get; set; }

        public DateTime StartTime { get; set; }


        public DateTime EndTime { get; set; }


        [StringLength(400, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string Comment { get; set; }

    }
}
