using AutoMapper;
using BjBygg.Application.Commands.BaseEntityCommands.Create;
using BjBygg.Application.Common.Dto;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.Create
{
    public class CreateInboundEmailPasswordCommand : CreateCommand<InboundEmailPasswordDto>
    {
        public string Password { get; set; }
    }
    public class CreateInboundEmailPasswordCommandHandler :
        CreateCommandHandler<InboundEmailPassword, CreateInboundEmailPasswordCommand, InboundEmailPasswordDto>
    {
        public CreateInboundEmailPasswordCommandHandler(AppIdentityDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
