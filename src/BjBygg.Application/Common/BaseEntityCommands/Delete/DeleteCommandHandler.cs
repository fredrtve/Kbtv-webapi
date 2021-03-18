using BjBygg.Application.Common.Interfaces;
using BjBygg.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Common.BaseEntityCommands.Delete
{
    public abstract class DeleteCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand>
        where TEntity : BaseEntity where TCommand : DeleteCommand
    {
        private readonly IDbContext _dbContext;

        public DeleteCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(request.Id);

            if (entity == null) return Unit.Value;

            _dbContext.Set<TEntity>().Remove(entity);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
