using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Delete;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.DocumentTypeCommands
{
    public class DeleteDocumentTypeCommand : DeleteCommand { }

    public class DeleteDocumentTypeCommandValidator : DeleteCommandValidator<DeleteDocumentTypeCommand> { }

    public class DeleteDocumentTypeCommandHandler : DeleteCommandHandler<DocumentType, DeleteDocumentTypeCommand>
    {
        public DeleteDocumentTypeCommandHandler(IAppDbContext dbContext) :
            base(dbContext)
        { }
    }
}