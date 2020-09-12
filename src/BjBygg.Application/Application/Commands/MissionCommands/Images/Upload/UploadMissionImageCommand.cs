using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using CleanArchitecture.SharedKernel;
using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommand : CreateCommand
    {
        public DisposableList<BasicFileStream> Files { get; set; }
        public string MissionId { get; set; }

    }
}
