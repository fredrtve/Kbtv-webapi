using AutoMapper;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;

namespace BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Create
{
    public class CreateInboundEmailPasswordCommand : CreateCommand
    {
        public string Password { get; set; }
    }
    public class CreateInboundEmailPasswordCommandHandler :
        CreateCommandHandler<InboundEmailPassword, CreateInboundEmailPasswordCommand>
    {
        public CreateInboundEmailPasswordCommandHandler(IAppIdentityDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
