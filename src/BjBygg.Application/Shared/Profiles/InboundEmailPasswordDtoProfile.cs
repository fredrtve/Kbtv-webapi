using AutoMapper;
using BjBygg.Application.Shared.Dto;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Shared
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
