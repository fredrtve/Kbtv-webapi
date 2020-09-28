using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Common.Profiles
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
