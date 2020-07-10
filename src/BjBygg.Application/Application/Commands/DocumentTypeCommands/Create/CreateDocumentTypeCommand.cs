using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.DocumentTypeCommands.Create
{
    public class CreateDocumentTypeCommand : CreateCommand<DocumentTypeDto>
    {
        public string Name { get; set; }
    }
    public class CreateDocumentTypeCommandHandler : CreateCommandHandler<DocumentType, CreateDocumentTypeCommand, DocumentTypeDto>
    {
        public CreateDocumentTypeCommandHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
