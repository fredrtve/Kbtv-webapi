using BjBygg.Application.Common.BaseEntityCommands.DeleteRange;
using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;

namespace BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.DeleteRange
{
    public class DeleteRangeInboundEmailPasswordHandler :
        DeleteRangeHandler<InboundEmailPassword, DeleteRangeInboundEmailPasswordCommand>
    {
        public DeleteRangeInboundEmailPasswordHandler(IAppIdentityDbContext dbContext) :
            base(dbContext)
        { }

    }
}
