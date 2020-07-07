using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Update
{
    public class UpdateDocumentTypeCommand : UpdateCommand<DocumentTypeDto>
    {
        public string Name { get; set; }
    }
    public class UpdateDocumentTypeCommandHandler : UpdateCommandHandler<DocumentType, UpdateDocumentTypeCommand, DocumentTypeDto>
    {
        public UpdateDocumentTypeCommandHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
