using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Create
{
    public class CreateDocumentTypeProfile : Profile
    {
        public CreateDocumentTypeProfile()
        {
            CreateMap<CreateDocumentTypeCommand, DocumentType>();
        }
    }
}
