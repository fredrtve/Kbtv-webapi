using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Update;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Update
{
    public class UpdateDocumentTypeHandler : UpdateHandler<DocumentType, UpdateDocumentTypeCommand, DocumentTypeDto>
    {
        public UpdateDocumentTypeHandler(AppDbContext dbContext, IMapper mapper) : 
            base (dbContext, mapper) {}
    }
}
