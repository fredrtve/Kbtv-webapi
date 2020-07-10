using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.DocumentTypeCommands
{
    public class DeleteRangeDocumentTypeCommand : DeleteRangeCommand
    {
    }
    public class DeleteRangeDocumentTypeCommandHandler : DeleteRangeHandler<DocumentType, DeleteRangeDocumentTypeCommand>
    {
        public DeleteRangeDocumentTypeCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}
