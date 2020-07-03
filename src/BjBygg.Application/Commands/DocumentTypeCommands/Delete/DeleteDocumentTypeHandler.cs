using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Delete
{
    public class DeleteDocumentTypeHandler : DeleteHandler<DocumentType, DeleteDocumentTypeCommand>
    {
        public DeleteDocumentTypeHandler(AppDbContext dbContext) : 
            base(dbContext) {}
    }
}
