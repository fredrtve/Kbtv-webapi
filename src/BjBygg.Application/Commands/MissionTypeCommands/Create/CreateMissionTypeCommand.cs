using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.MissionTypeCommands.Create
{
    public class CreateMissionTypeCommand : CreateCommand<MissionTypeDto>
    {
        [Required(ErrorMessage = "{0} må fylles ut.")]
        [StringLength(45, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        [Display(Name = "Navn")]
        public string Name { get; set; }
    }
}
