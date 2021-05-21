using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Common.Profiles
{
    public class LeaderSettingsDtoProfile : Profile
    {
        public LeaderSettingsDtoProfile()
        {
            CreateMap<LeaderSettings, LeaderSettingsDto>();
            CreateMap<LeaderSettingsDto, LeaderSettings>();
        }
    }
}
