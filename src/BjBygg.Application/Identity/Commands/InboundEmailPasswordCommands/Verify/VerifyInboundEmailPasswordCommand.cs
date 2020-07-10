using BjBygg.Application.Identity.Common.Interfaces;
using BjBygg.Application.Identity.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Verify
{
    public class VerifyInboundEmailPasswordCommand : IRequest<bool>
    {
        public string Password { get; set; }
    }

    public class VerifyInboundEmailPasswordCommandHandler : IRequestHandler<VerifyInboundEmailPasswordCommand, bool>
    {
        private readonly IAppIdentityDbContext _dbContext;

        public VerifyInboundEmailPasswordCommandHandler(IAppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(VerifyInboundEmailPasswordCommand request, CancellationToken cancellationToken)
        {
            var match = await _dbContext.Set<InboundEmailPassword>().AnyAsync(x => x.Password == request.Password);
            return match;
        }
    }
}
