using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.BaseEntityCommands.Delete
{
    public abstract class DeleteCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, bool>
        where TEntity : BaseEntity where TCommand : DeleteCommand
    {
        private readonly DbContext _dbContext;

        public DeleteCommandHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(request.Id);

            if (entity == null) throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            _dbContext.Set<TEntity>().Remove(entity); 
            
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
