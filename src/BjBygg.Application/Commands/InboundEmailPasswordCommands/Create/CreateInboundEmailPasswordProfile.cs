using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.Create
{
    public class CreateInboundEmailPasswordProfile : Profile
    {
        public CreateInboundEmailPasswordProfile()
        {
            CreateMap<CreateInboundEmailPasswordCommand, InboundEmailPassword>();
        }
    }
}
