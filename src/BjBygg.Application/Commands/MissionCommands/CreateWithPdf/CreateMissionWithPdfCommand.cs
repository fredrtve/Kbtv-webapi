
using BjBygg.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BjBygg.Application.Commands.MissionCommands.CreateWithPdf
{
    public class CreateMissionWithPdfCommand : IRequest<MissionDto>
    {
        [Required]
        [JsonIgnore]
        public IFormFileCollection Files { get; set; }

    }
}
