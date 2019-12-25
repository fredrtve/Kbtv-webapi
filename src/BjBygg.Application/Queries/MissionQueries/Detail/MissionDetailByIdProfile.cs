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
                .ForMember(
                    dest => dest.MissionTypeName,
                    act => act.MapFrom(x => x.MissionType.Name))
                .ForMember(
                    dest => dest.Employer,
                    act => act.MapFrom(x => x.Employer))
                .ForMember(
                    dest => dest.MissionNotes,
                    act => act.MapFrom(x => x.MissionNotes))
                .ForMember(
                    dest => dest.MissionImages,
                    act => act.MapFrom(x => x.MissionImages));

            CreateMap<MissionNote, MissionNoteDto>();
            CreateMap<Employer, MissionEmployerDto>();
            CreateMap<MissionImage, MissionImageDto>();
        }
    }
}
