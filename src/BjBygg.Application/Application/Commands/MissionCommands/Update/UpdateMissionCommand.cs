using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common.BaseEntityCommands.Update;
using BjBygg.SharedKernel;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommand : UpdateCommand, IContactable, IAddress
    {
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public bool? Finished { get; set; }
        public MissionTypeDto MissionType { get; set; }
        public string? MissionTypeId { get; set; }
        public EmployerDto Employer { get; set; }
        public string? EmployerId { get; set; }
    }
}
