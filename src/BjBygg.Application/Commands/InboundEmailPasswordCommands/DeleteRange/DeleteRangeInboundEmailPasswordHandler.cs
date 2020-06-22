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
    public class DeleteRangeInboundEmailPasswordHandler : IRequestHandler<DeleteRangeInboundEmailPasswordCommand, bool>
    {
        private readonly AppIdentityDbContext _dbContext;

        public DeleteRangeInboundEmailPasswordHandler(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteRangeInboundEmailPasswordCommand request, CancellationToken cancellationToken)
        {
            var passwords = _dbContext.Set<InboundEmailPassword>().Where(x => request.Ids.Contains(x.Id)).ToList();

            if (passwords.Count() == 0) throw new EntityNotFoundException($"No entities found with given id's");

            _dbContext.Set<InboundEmailPassword>().RemoveRange(passwords);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
