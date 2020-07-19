using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents
{
    public class DeleteRangeMissionDocumentCommand : DeleteRangeCommand {}
    public class DeleteRangeMissionDocumentCommandValidator : DeleteRangeCommandValidator<DeleteRangeMissionDocumentCommand> {}
    public class DeleteRangeMissionDocumentCommandHandler :
        DeleteRangeCommandHandler<MissionDocument, DeleteRangeMissionDocumentCommand>
    {
        public DeleteRangeMissionDocumentCommandHandler(IAppDbContext dbContext) :
            base(dbContext) {}
    }
}
