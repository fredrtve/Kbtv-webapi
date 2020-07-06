
using BjBygg.Application.Common;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BjBygg.Application.Commands.MissionCommands.CreateWithPdf
{
    public class CreateMissionWithPdfCommand : IRequest<MissionDto>
    {
        public CreateMissionWithPdfCommand(){}
        public CreateMissionWithPdfCommand(DisposableList<BasicFileStream> files)
        {
            Files = files;
        }

        [Required]
        [JsonIgnore]
        public DisposableList<BasicFileStream> Files { get; set; }

    }
}
