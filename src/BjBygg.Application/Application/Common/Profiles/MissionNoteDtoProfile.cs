using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Common.Profiles
{
    public class MissionNoteDtoProfile : Profile
    {
        public MissionNoteDtoProfile()
        {
            CreateMap<MissionNote, MissionNoteDto>();
            CreateMap<MissionNoteDto, MissionNote>();
        }
    }
}
