using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Identity.Common.Models;

namespace BjBygg.Application.Identity.Common
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
