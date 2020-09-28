using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Common.Profiles
{
    class MissionImageDtoProfile : Profile
    {
        public MissionImageDtoProfile()
        {
            CreateMap<MissionImage, MissionImageDto>();
            CreateMap<MissionImageDto, MissionImage>();
        }
    }
}
