using BjBygg.Application.Application.Common.Dto;
using CleanArchitecture.SharedKernel;
using MediatR;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload
{
    public class UploadMissionDocumentCommand : IRequest<MissionDocumentDto>
    {
        public BasicFileStream File { get; set; }
        public int MissionId { get; set; }
        public DocumentTypeDto DocumentType { get; set; }
    }
}
