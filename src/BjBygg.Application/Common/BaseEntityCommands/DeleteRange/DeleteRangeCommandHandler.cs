using BjBygg.Application.Common.Interfaces;
using BjBygg.SharedKernel;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Common.BaseEntityCommands.DeleteRange
{
    public abstract class DeleteRangeCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand>
        where TEntity : BaseEntity where TCommand : DeleteRangeCommand
    {
        private readonly IDbContext _dbContext;

        public DeleteRangeCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entities = _dbContext.Set<TEntity>().Where(x => request.Ids.Contains(x.Id)).ToList();
            _dbContext.Set<TEntity>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}