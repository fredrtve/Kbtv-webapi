using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.ToggleMissionFinish
{
    public class ToggleMissionFinishHandler : IRequestHandler<ToggleMissionFinishCommand>
    {
        private readonly IAppDbContext _dbContext;

        public ToggleMissionFinishHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ToggleMissionFinishCommand request, CancellationToken cancellationToken)
        {
            var dbMission = await _dbContext.Set<Mission>().FindAsync(request.Id);

            if (dbMission == null) throw new EntityNotFoundException(nameof(Mission), request.Id);

            dbMission.Finished = !dbMission.Finished;

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
