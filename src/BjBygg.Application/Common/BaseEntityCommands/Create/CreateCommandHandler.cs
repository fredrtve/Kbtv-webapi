using AutoMapper;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Common.BaseEntityCommands.Create
{
    public abstract class CreateCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand>
        where TEntity : BaseEntity where TCommand : CreateCommand
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);

            _dbContext.Set<TEntity>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
