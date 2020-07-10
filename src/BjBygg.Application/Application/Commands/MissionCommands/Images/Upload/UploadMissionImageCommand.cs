using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.SharedKernel;
using MediatR;
using System.Collections.Generic;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommand : IRequest<List<MissionImageDto>>
    {
        public DisposableList<BasicFileStream> Files { get; set; }
        public int MissionId { get; set; }

    }
}
