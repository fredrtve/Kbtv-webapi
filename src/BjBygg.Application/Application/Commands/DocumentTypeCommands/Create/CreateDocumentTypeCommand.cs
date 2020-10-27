using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;

namespace BjBygg.Application.Application.Commands.DocumentTypeCommands.Create
{
    public class CreateDocumentTypeCommand : CreateCommand, IName
    {
        public string Name { get; set; }
    }
    public class CreateDocumentTypeCommandHandler : CreateCommandHandler<DocumentType, CreateDocumentTypeCommand>
    {
        public CreateDocumentTypeCommandHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
