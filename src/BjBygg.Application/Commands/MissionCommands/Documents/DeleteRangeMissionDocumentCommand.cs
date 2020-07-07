using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.MissionCommands.Documents
{
    public class DeleteRangeMissionDocumentCommand : DeleteRangeCommand
    {
    }

    public class DeleteRangeMissionDocumentCommandHandler :
        DeleteRangeHandler<MissionDocument, DeleteRangeMissionDocumentCommand>
    {
        public DeleteRangeMissionDocumentCommandHandler(AppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
