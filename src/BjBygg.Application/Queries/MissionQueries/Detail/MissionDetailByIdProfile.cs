using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BjBygg.Application.Queries.MissionQueries.Detail
{
    public class MissionDetailByIdProfile : Profile
    {
        public MissionDetailByIdProfile()
        {
            CreateMap<Mission, MissionDetailByIdResponse>()
                .ForMember(x => x.Mission, act => act.MapFrom(x => x))
  
                .ForMember(
                    dest => dest.MissionNotes,
                    act => act.MapFrom(x => x.MissionNotes))
                .ForMember(
                    dest => dest.MissionImages,
                    act => act.MapFrom(x => x.MissionImages))
                .ForMember(
                    dest => dest.MissionDocuments,
                    act => act.MapFrom(x => x.MissionDocuments));

            CreateMap<Mission, MissionDto>()
                .ForMember(
                    dest => dest.MissionType,
                    act => act.MapFrom(x => x.MissionType))
                .ForMember(
                    dest => dest.Employer,
                    act => act.MapFrom(x => x.Employer));

            CreateMap<MissionNote, MissionDetailNoteDto>();
            CreateMap<Employer, EmployerDto>();
            CreateMap<MissionType, MissionTypeDto>();
            CreateMap<MissionImage, MissionImageDto>();

            CreateMap<MissionDocument, MissionDocumentDto>()
                .ForMember(dest => dest.DocumentType, act => act.MapFrom(x => x.DocumentType));

            CreateMap<DocumentType, DocumentTypeDto>();
        }
    }
}
