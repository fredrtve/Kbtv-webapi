using BjBygg.Application.Common.BaseEntityCommands.Create;
using System.IO;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommand : CreateCommand
    {
        public Stream File { get; set; }
        public string FileExtension { get; set; }
        public string MissionId { get; set; }
        public string Name { get; set; }
    }
}
