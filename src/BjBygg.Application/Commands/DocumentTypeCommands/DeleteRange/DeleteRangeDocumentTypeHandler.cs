using CleanArchitecture.Core.Entities;
using BjBygg.Application.Commands.Shared.DeleteRange;
using CleanArchitecture.Infrastructure.Data;


namespace BjBygg.Application.Commands.DocumentTypeCommands.DeleteRange
{
    public class DeleteRangeDocumentTypeHandler : DeleteRangeHandler<DocumentType, DeleteRangeDocumentTypeCommand>
    {
        public DeleteRangeDocumentTypeHandler(AppDbContext dbContext) : base(dbContext){}
    }
}
