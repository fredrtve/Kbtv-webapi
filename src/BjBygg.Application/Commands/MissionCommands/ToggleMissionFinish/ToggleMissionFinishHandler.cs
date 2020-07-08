using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.ToggleMissionFinish
{
    public class ToggleMissionFinishHandler : IRequestHandler<ToggleMissionFinishCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public ToggleMissionFinishHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(ToggleMissionFinishCommand request, CancellationToken cancellationToken)
        {
            var dbMission = await _dbContext.Set<Mission>().FindAsync(request.Id);

            if (dbMission == null) throw new EntityNotFoundException(nameof(Mission), request.Id);

            dbMission.Finished = !dbMission.Finished;

            await _dbContext.SaveChangesAsync();

            return dbMission.Finished;
        }
    }
}
