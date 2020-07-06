using AutoMapper;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.BaseEntityCommands.Create
{
    public abstract class CreateCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, int>
        where TEntity : BaseEntity where TCommand : CreateCommand
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCommandHandler(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);

            _dbContext.Set<TEntity>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }
    }
}
