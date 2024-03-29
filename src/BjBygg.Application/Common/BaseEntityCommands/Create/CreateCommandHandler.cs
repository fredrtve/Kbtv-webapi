using AutoMapper;
using BjBygg.Application.Common.Interfaces;
using BjBygg.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Common.BaseEntityCommands.Create
{
    public abstract class CreateCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand>
        where TEntity : BaseEntity where TCommand : CreateCommand
    {
        protected readonly IDbContext _dbContext;
        protected readonly IMapper _mapper;

        public CreateCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);

            _dbContext.Set<TEntity>().Add(entity);

            await OnBeforeSavingAsync(request, entity);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        protected virtual async Task OnBeforeSavingAsync(TCommand request, TEntity entity) { }
    }
}
