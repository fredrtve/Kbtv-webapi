using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common.Dto;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.Create
{
    public class CreateInboundEmailPasswordCommand : CreateCommand<InboundEmailPasswordDto>
    {
        [Required(ErrorMessage = "{0} må fylles ut.")]
        [Display(Name = "Passord")]
        [StringLength(50, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string Password { get; set; }
    }
}
