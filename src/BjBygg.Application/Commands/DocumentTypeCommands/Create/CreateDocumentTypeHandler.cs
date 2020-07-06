using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
namespace BjBygg.Application.Commands.DocumentTypeCommands.Create
{
    public class CreateDocumentTypeHandler : CreateHandler<DocumentType, CreateDocumentTypeCommand, DocumentTypeDto>
    {
        public CreateDocumentTypeHandler(AppDbContext dbContext, IMapper mapper) : 
            base(dbContext, mapper) {}
    }
}
