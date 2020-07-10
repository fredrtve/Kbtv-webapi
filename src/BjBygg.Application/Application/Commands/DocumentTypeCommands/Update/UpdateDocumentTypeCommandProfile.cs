using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.DocumentTypeCommands.Update
{
    class UpdateDocumentTypeCommandProfile : Profile
    {
        public UpdateDocumentTypeCommandProfile()
        {
            CreateMap<UpdateDocumentTypeCommand, DocumentType>();
        }
    }
}
