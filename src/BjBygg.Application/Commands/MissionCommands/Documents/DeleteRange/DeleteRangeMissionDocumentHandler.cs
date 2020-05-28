using BjBygg.Application.Commands.Shared.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;


namespace BjBygg.Application.Commands.MissionCommands.Documents.DeleteRange
{
    public class DeleteRangeMissionDocumentHandler : DeleteRangeHandler<MissionDocument, DeleteRangeMissionDocumentCommand>
    {
        public DeleteRangeMissionDocumentHandler(AppDbContext dbContext) : base(dbContext){}
    }
}
