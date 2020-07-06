using AutoMapper;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.BaseEntityCommands.Update
{
    public abstract class UpdateHandler<TEntity, TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TEntity : BaseEntity where TCommand : UpdateCommand<TResponse>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateHandler(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var dbEntity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbEntity == null)
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            foreach (var property in request.GetType().GetProperties())
            {
                if (property.Name == "Id") continue;
                dbEntity.GetType().GetProperty(property.Name).SetValue(dbEntity, property.GetValue(request), null);
            }

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TResponse>(dbEntity);
        }
    }
}
