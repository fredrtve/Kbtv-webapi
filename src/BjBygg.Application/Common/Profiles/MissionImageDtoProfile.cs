using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Common
{
    class MissionImageDtoProfile : Profile
    {
        public MissionImageDtoProfile()
        {
            CreateMap<MissionImage, MissionImageDto>();
        }
    }
}
