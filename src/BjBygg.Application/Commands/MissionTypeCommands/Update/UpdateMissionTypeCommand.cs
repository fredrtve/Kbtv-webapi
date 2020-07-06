using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Common;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.MissionTypeCommands.Update
{
    public class UpdateMissionTypeCommand : UpdateCommand<MissionTypeDto>
    {
        [Required(ErrorMessage = "{0} må fylles ut.")]
        [StringLength(45, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        [Display(Name = "Navn")]
        public string Name { get; set; }
    }
}
