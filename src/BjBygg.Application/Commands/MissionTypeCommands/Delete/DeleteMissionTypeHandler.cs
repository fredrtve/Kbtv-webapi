using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionTypeCommands.Delete
{
    public class DeleteMissionTypeHandler : IRequestHandler<DeleteMissionTypeCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteMissionTypeHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteMissionTypeCommand request, CancellationToken cancellationToken)
        {
            var MissionType = await _dbContext.Set<MissionType>().FindAsync(request.Id);

            if (MissionType == null) throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            _dbContext.Set<MissionType>().Remove(MissionType);      
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
