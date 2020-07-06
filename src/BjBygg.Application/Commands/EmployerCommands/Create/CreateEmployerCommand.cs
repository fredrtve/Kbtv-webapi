using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Commands.EmployerCommands.Create
{
    public class CreateEmployerCommand : CreateCommand<EmployerDto>
    {
        [Required(ErrorMessage = "{0} må fylles ut.")]
        [Display(Name = "Navn")]
        [StringLength(50, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string Name { get; set; }
        [Display(Name = "Mobilnr")]
        [StringLength(12, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Adresse")]
        [StringLength(100, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string? Address { get; set; }
    }
}
