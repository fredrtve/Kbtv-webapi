using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.DocumentTypeCommands
{
    public class DeleteRangeDocumentTypeCommand : DeleteRangeCommand 
    {
    }
    public class DeleteRangeDocumentTypeCommandHandler : DeleteRangeHandler<DocumentType, DeleteRangeDocumentTypeCommand>
    {
        public DeleteRangeDocumentTypeCommandHandler(AppDbContext dbContext) : 
            base(dbContext) {}
    }
}
