using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.DeleteRange
{
    public class DeleteRangeInboundEmailPasswordHandler : 
        DeleteRangeHandler<InboundEmailPassword, DeleteRangeInboundEmailPasswordCommand>
    {
        public DeleteRangeInboundEmailPasswordHandler(AppIdentityDbContext dbContext) : 
            base(dbContext) {}

    }
}
