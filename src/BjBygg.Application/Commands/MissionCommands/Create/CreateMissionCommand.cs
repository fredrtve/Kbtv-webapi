
using BjBygg.Application.Common;
using CleanArchitecture.SharedKernel;
using MediatR;
using System.Text.Json.Serialization;

namespace BjBygg.Application.Commands.MissionCommands.Create
{
    public class CreateMissionCommand : IRequest<MissionDto>
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public MissionTypeDto? MissionType { get; set; }
        public EmployerDto? Employer { get; set; }

        [JsonIgnore]
        public BasicFileStream? Image { get; set; }
    }
}
