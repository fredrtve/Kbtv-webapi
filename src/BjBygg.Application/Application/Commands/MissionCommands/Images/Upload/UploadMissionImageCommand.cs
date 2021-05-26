using BjBygg.Application.Common.BaseEntityCommands.Create;
using System.IO;

namespace BjBygg.Application.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommand : CreateCommand
    {
        public Stream File { get; set; }
        public string FileExtension { get; set; }
        public string MissionId { get; set; }

    }
}
