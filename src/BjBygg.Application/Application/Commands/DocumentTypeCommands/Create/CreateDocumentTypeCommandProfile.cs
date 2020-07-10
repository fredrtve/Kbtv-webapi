using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.DocumentTypeCommands.Create
{
    public class CreateDocumentTypeCommandProfile : Profile
    {
        public CreateDocumentTypeCommandProfile()
        {
            CreateMap<CreateDocumentTypeCommand, DocumentType>();
        }
    }
}
