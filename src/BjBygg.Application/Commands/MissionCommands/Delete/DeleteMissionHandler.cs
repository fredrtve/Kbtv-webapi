using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Delete
{
    public class DeleteMissionHandler : IRequestHandler<DeleteMissionCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteMissionHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = await _dbContext.Set<Mission>().FindAsync(request.Id);
            if (mission == null) return false;

            _dbContext.Set<Mission>().Remove(mission);      
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
