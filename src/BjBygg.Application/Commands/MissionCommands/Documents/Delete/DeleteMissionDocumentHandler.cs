using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Documents.Delete
{
    public class DeleteMissionDocumentHandler : DeleteHandler<MissionDocument, DeleteMissionDocumentCommand>
    {
        public DeleteMissionDocumentHandler(AppDbContext dbContext) :
            base(dbContext) { }
    }
}
