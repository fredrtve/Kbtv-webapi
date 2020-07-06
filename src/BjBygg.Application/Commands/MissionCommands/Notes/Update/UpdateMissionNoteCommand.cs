using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Common;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.MissionCommands.Notes.Update
{
    public class UpdateMissionNoteCommand : UpdateCommand<MissionNoteDto>
    {
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
