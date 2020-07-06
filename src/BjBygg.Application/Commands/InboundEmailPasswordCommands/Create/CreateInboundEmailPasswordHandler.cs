using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common.Dto;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.Create
{
    public class CreateInboundEmailPasswordHandler : 
        CreateHandler<InboundEmailPassword, CreateInboundEmailPasswordCommand, InboundEmailPasswordDto>
    {
        public CreateInboundEmailPasswordHandler(AppIdentityDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper) {}
    }
}
