using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common.BaseEntityCommands.Update;
using CleanArchitecture.SharedKernel;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommand : UpdateCommand
    {
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public bool? Finished { get; set; }
        public MissionTypeDto MissionType { get; set; }
        public EmployerDto Employer { get; set; }
        public Boolean? DeleteCurrentImage { get; set; }
    }
}
