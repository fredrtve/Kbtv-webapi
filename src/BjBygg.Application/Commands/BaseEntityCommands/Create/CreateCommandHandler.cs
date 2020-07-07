using AutoMapper;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.BaseEntityCommands.Create
{
    public class CreateCommandHandler<TEntity, TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TEntity : BaseEntity where TCommand : CreateCommand<TResponse>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCommandHandler(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(request);

            _dbContext.Set<TEntity>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TResponse>(entity);
        }
    }
}
