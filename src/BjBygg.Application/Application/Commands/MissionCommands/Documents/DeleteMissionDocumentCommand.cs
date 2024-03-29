using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Delete;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionCommands.Documents
{
    public class DeleteMissionDocumentCommand : DeleteCommand { }
    public class DeleteMissionDocumentCommandValidator : DeleteCommandValidator<DeleteMissionDocumentCommand> { }
    public class DeleteMissionDocumentHandler : DeleteCommandHandler<MissionDocument, DeleteMissionDocumentCommand>
    {
        public DeleteMissionDocumentHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
