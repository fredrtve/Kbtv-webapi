using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Common
{
    public class DocumentTypeDtoProfile : Profile
    {
        public DocumentTypeDtoProfile()
        {
            CreateMap<DocumentType, DocumentTypeDto>();
            CreateMap<DocumentTypeDto, DocumentType>();
        }
    }
}
