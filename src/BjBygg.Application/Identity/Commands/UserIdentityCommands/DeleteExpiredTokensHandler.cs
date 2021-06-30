using BjBygg.Application.Identity.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.DeleteOldTokens
{
    public class DeleteExpiredTokensCommand : IRequest { }
    public class DeleteExpiredTokensHandler : IRequestHandler<DeleteExpiredTokensCommand>
    {
        private readonly IAppIdentityDbContext _dbContext;

        public DeleteExpiredTokensHandler(IAppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(DeleteExpiredTokensCommand request, CancellationToken cancellationToken)
        {
            _dbContext.RefreshTokens.RemoveRange(_dbContext.RefreshTokens.Where(x => x.Expires < DateTime.UtcNow));
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
