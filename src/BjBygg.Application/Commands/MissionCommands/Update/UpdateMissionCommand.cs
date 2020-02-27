using BjBygg.Application.Queries.MissionQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommand : IRequest<MissionDto>
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} må fylles ut.")]
        [Display(Name = "Adresse")]
        [StringLength(100, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string Address { get; set; }

        [Display(Name = "Mobilnr")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(12, ErrorMessage = "{0} må være mellom {2} og {1} tegn.", MinimumLength = 4)]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Beskrivelse")]
        [StringLength(400, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string? Description { get; set; }

        [Display(Name = "Ferdig?")]
        public bool? Finished { get; set; }

        public MissionTypeDto MissionType { get; set; }

        public EmployerDto Employer { get; set; }

    }
}
