using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.SharedKernel;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.Shared.DeleteRange
{
    public abstract class DeleteRangeHandler<T, C> : IRequestHandler<C, bool> 
        where T : BaseEntity where C : DeleteRangeCommand
    {
        private readonly AppDbContext _dbContext;

        public DeleteRangeHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(C request, CancellationToken cancellationToken)
        {
            var entities = _dbContext.Set<T>().Where(x => request.Ids.Contains(x.Id)).ToList();

            if (entities.Count() == 0) throw new EntityNotFoundException($"No entities found with given id's");

            _dbContext.Set<T>().RemoveRange(entities);      
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}