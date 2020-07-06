using BjBygg.Application.Commands.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.DocumentTypeCommands
{
    public class DeleteDocumentTypeCommand : DeleteCommand
    {
    }

    public class DeleteDocumentTypeCommandHandler : DeleteCommandHandler<DocumentType, DeleteDocumentTypeCommand>
    {
        public DeleteDocumentTypeCommandHandler(AppDbContext dbContext) :
            base(dbContext){}
    }
}
