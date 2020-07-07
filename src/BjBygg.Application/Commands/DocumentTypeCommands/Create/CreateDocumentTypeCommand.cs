using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Create
{
    public class CreateDocumentTypeCommand : CreateCommand<DocumentTypeDto>
    {
        public string Name { get; set; }
    }
    public class CreateDocumentTypeCommandHandler : CreateCommandHandler<DocumentType, CreateDocumentTypeCommand, DocumentTypeDto>
    {
        public CreateDocumentTypeCommandHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
