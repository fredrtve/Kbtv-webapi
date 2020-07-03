using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Commands.MissionCommands.Documents.Upload
{
    class UploadMissionDocumentProfile : Profile
    {
        public UploadMissionDocumentProfile()
        {
            CreateMap<UploadMissionDocumentCommand, MissionDocument>()
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
                .ForMember(dest => dest.FileURL, opt => opt.Ignore())
                .ForSourceMember(src => src.File, dest => dest.DoNotValidate());

            CreateMap<MissionDocument, MissionDocumentDto>()
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType));

            CreateMap<DocumentTypeDto, DocumentType>();
            CreateMap<DocumentType, DocumentTypeDto>();
        }
    }
}
