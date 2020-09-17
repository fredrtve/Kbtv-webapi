using AutoMapper;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Common.BaseEntityCommands.Update
{
    public abstract class UpdateCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand>
        where TEntity : BaseEntity where TCommand : UpdateCommand
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var dbEntity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbEntity == null)
                throw new EntityNotFoundException(nameof(TEntity), request.Id);

            foreach (var property in request.GetType().GetProperties())
            {
                if(property.Name == "Id") continue;
                dbEntity.GetType().GetProperty(property.Name).SetValue(dbEntity, property.GetValue(request), null);
            }

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        
    }
}
