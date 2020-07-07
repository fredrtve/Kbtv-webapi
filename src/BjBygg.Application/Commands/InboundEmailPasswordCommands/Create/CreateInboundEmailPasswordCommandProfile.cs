using AutoMapper;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.Create
{
    public class CreateInboundEmailPasswordCommandProfile : Profile
    {
        public CreateInboundEmailPasswordCommandProfile()
        {
            CreateMap<CreateInboundEmailPasswordCommand, InboundEmailPassword>();
        }
    }
}
