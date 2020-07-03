using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Commands.DocumentTypeCommands.Update
{
    class UpdateDocumentTypeProfile : Profile
    {
        public UpdateDocumentTypeProfile()
        {
            CreateMap<UpdateDocumentTypeCommand, DocumentType>();
        }
    }
}
