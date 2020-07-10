using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.SharedKernel;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommand : IRequest<MissionDto>
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public bool? Finished { get; set; }
        public MissionTypeDto MissionType { get; set; }
        public EmployerDto Employer { get; set; }
        public Boolean? DeleteCurrentImage { get; set; }

        [JsonIgnore]
        public BasicFileStream? Image { get; set; }
    }
}
