using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.BaseEntityCommands.DeleteRange
{
    public abstract class DeleteRangeHandler<TEntity, TCommand> : IRequestHandler<TCommand>
        where TEntity : BaseEntity where TCommand : DeleteRangeCommand
    {
        private readonly DbContext _dbContext;

        public DeleteRangeHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entities = _dbContext.Set<TEntity>().Where(x => request.Ids.Contains(x.Id)).ToList();

            if (entities.Count() == 0) throw new EntityNotFoundException($"No entities found with given id's");

            _dbContext.Set<TEntity>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}