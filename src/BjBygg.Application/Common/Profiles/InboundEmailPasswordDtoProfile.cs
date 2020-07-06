using AutoMapper;
using BjBygg.Application.Common.Dto;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Common
{
    public class InboundEmailPasswordDtoProfile : Profile
    {
        public InboundEmailPasswordDtoProfile()
        {
            CreateMap<InboundEmailPassword, InboundEmailPasswordDto>();

            CreateMap<InboundEmailPasswordDto, InboundEmailPassword>();
        }
    }
}
