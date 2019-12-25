using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.MissionTypeCommands.Create
{
    public class CreateMissionTypeCommand : IRequest
    {
        [Required(ErrorMessage = "{0} må fylles ut.")]
        [StringLength(45, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        [Display(Name = "Navn")]
        public string Name { get; set; }
    }
}
