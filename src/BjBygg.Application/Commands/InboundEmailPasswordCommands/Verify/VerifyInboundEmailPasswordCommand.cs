using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.InboundEmailPasswordCommands.Verify
{
    public class VerifyInboundEmailPasswordCommand : IRequest<bool>
    {
        public string Password { get; set; }
    }

    public class VerifyInboundEmailPasswordCommandHandler : IRequestHandler<VerifyInboundEmailPasswordCommand, bool>
    {
        private readonly AppIdentityDbContext _dbContext;

        public VerifyInboundEmailPasswordCommandHandler(AppIdentityDbContext dbContext)
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
