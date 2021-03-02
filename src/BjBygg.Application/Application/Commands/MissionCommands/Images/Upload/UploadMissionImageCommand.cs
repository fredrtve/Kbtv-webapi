using BjBygg.Application.Common.BaseEntityCommands.Create;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.SharedKernel;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommand : CreateCommand
    {
        public BasicFileStream File { get; set; }
        public string MissionId { get; set; }

    }
}
