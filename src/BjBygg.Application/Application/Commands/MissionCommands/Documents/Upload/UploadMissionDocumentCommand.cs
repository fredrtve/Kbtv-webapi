using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using CleanArchitecture.SharedKernel;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommand : CreateCommand
    {
        public BasicFileStream File { get; set; }
        public string MissionId { get; set; }
        public string DocumentTypeId { get; set; }
        public DocumentTypeDto DocumentType { get; set; }
    }
}
