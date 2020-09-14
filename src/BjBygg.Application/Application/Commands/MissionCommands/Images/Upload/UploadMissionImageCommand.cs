using CleanArchitecture.SharedKernel;
using MediatR;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommand : IRequest
    {
        public DisposableList<BasicFileStream> Files { get; set; }
        public string MissionId { get; set; }

    }
}
