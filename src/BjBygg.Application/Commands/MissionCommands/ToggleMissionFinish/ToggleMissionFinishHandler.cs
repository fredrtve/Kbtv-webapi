using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.ToggleMissionFinish
{
    public class ToggleMissionFinishHandler : IRequestHandler<ToggleMissionFinishCommand, bool>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ToggleMissionFinishHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ToggleMissionFinishCommand request, CancellationToken cancellationToken)
        {
            var mission = await _dbContext.Set<Mission>().FindAsync(request.Id);

            if (mission == null) throw new EntityNotFoundException($"Mission does not exist with id {request.Id}");

            if (mission.Finished) mission.Finished = false;
            else mission.Finished = true;

            _dbContext.Entry(mission).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return mission.Finished;
        }
    }
}
