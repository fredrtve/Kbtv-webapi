using BjBygg.Application.Commands.BaseEntityCommands.DeleteRange;
using CleanArchitecture.Infrastructure.Identity;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.DeleteRange
{
    public class DeleteRangeInboundEmailPasswordHandler :
        DeleteRangeHandler<InboundEmailPassword, DeleteRangeInboundEmailPasswordCommand>
    {
        public DeleteRangeInboundEmailPasswordHandler(AppIdentityDbContext dbContext) :
            base(dbContext)
        { }

    }
}
