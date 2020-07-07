using BjBygg.Application.Common;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace BjBygg.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommand : IRequest<MissionDocumentDto>
    {
        public BasicFileStream File { get; set; }
        public int MissionId { get; set; }
        public DocumentTypeDto DocumentType { get; set; }
    }
}
