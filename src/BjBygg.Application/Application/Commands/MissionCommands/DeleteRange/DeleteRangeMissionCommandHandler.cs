using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.DeleteRange
{
    public class DeleteRangeMissionCommandHandler : IRequestHandler<DeleteRangeMissionCommand>
    {
        private readonly IAppDbContext _dbContext;

        public DeleteRangeMissionCommandHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteRangeMissionCommand request, CancellationToken cancellationToken)
        {
            var missions = await _dbContext.Set<Mission>().Where(x => request.Ids.Contains(x.Id)).ToListAsync();

            foreach (var mission in missions) mission.Position = null;

            _dbContext.Set<Mission>().RemoveRange(missions);

            _dbContext.Set<MissionImage>().RemoveRange(
                await _dbContext.Set<MissionImage>().Where(x => request.Ids.Contains(x.MissionId)).ToListAsync());

            _dbContext.Set<MissionNote>().RemoveRange(
                await _dbContext.Set<MissionNote>().Where(x => request.Ids.Contains(x.MissionId)).ToListAsync());

            _dbContext.Set<MissionDocument>().RemoveRange(
                await _dbContext.Set<MissionDocument>().Where(x => request.Ids.Contains(x.MissionId)).ToListAsync());

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
