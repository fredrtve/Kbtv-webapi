
using BjBygg.Application.Common;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BjBygg.Application.Commands.MissionCommands.Create
{
    public class CreateMissionCommand : IRequest<MissionDto>
    {
        [Required(ErrorMessage = "{0} må fylles ut.")]
        [Display(Name = "Adresse")]
        [StringLength(100, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string Address { get; set; }

        [Display(Name = "Mobilnr")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(12, ErrorMessage = "{0} må være mellom {2} og {1} tegn.", MinimumLength = 4)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Beskrivelse")]
        [StringLength(400, ErrorMessage = "{0} kan maks være på {1} tegn.")]
        public string Description { get; set; }

        public MissionTypeDto? MissionType { get; set; }

        public EmployerDto? Employer { get; set; }

        [JsonIgnore]
        public BasicFileStream? Image { get; set; }
    }
}
