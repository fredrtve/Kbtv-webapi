
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using CleanArchitecture.SharedKernel;
using MediatR;
using System.Text.Json.Serialization;

namespace BjBygg.Application.Application.Commands.MissionCommands.Create
{
    public class CreateMissionCommand : CreateCommand
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string EmployerId { get; set; }
        public string MissionTypeId { get; set; }
        public MissionTypeDto? MissionType { get; set; }
        public EmployerDto? Employer { get; set; }
    }
}
