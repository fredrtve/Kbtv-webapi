using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;

namespace BjBygg.Application.Commands.DocumentTypeCommands.DeleteRange
{
    public class DeleteRangeDocumentTypeHandler : DeleteRangeCommandHandler<DocumentType, DeleteRangeDocumentTypeCommand>
    {
        public DeleteRangeDocumentTypeHandler(AppDbContext dbContext) : base(dbContext){}
    }
}
