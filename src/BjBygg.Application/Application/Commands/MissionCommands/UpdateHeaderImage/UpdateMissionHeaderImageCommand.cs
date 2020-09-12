using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.SharedKernel;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace BjBygg.Application.Application.Commands.MissionCommands.UpdateHeaderImage
{
    public class UpdateMissionHeaderImageCommand : IRequest
    {
        public string Id { get; set; }

        [JsonIgnore]
        public BasicFileStream Image { get; set; }
    }
}
