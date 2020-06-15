using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Create
{
    public class CreateMissionNoteCommand : IRequest<MissionNoteDto>
    {
        [Required]
        public int MissionId { get; set; }

        [StringLength(100, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        [Display(Name = "Tittel")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "{0} må fylles ut.")]
        [StringLength(400, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        [Display(Name = "Innhold")]
        public string Content { get; set; }

        [Display(Name = "Marker som viktig")]
        public bool? Pinned { get; set; }
    }
}
