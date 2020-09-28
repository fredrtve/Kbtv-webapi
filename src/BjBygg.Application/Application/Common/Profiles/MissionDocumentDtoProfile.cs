using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Common.Profiles
{
    public class MissionDocumentDtoProfile : Profile
    {
        public MissionDocumentDtoProfile()
        {
            CreateMap<MissionDocument, MissionDocumentDto>();

            CreateMap<MissionDocumentDto, MissionDocument>();
        }
    }
}
