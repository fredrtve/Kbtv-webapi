using AutoMapper;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.BaseEntityCommands.Update
{
    public class UpdateCommandHandler<TEntity, TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TEntity : BaseEntity where TCommand : UpdateCommand<TResponse>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(DbContext dbContext, IMapper mapper)
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


            try { await _dbContext.SaveChangesAsync(); }
            catch (Exception ex)
            {
                throw new BadRequestException($"Something went wrong when storing your request");
            }

            return _mapper.Map<TResponse>(dbEntity);
        }
    }
}
