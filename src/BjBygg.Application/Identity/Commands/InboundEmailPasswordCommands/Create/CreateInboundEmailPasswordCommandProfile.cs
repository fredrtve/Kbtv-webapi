using AutoMapper;
using BjBygg.Application.Identity.Common.Models;

namespace BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Create
{
    public class CreateInboundEmailPasswordCommandProfile : Profile
    {
        public CreateInboundEmailPasswordCommandProfile()
        {
            CreateMap<CreateInboundEmailPasswordCommand, InboundEmailPassword>();
        }
    }
}
