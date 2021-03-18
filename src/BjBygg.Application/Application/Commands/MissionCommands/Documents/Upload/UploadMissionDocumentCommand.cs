using BjBygg.Application.Common.BaseEntityCommands.Create;
using BjBygg.SharedKernel;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommand : CreateCommand
    {
        public BasicFileStream File { get; set; }
        public string MissionId { get; set; }
        public string Name { get; set; }
    }
}
