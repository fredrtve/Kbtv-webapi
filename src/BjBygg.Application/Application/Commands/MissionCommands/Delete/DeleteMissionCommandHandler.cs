using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Delete
{
    public class DeleteMissionCommandHandler : IRequestHandler<DeleteMissionCommand>
    {
        private readonly IAppDbContext _dbContext;

        public DeleteMissionCommandHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = await _dbContext.Set<Mission>().FindAsync(request.Id);

             if (mission == null) return Unit.Value;

            _dbContext.Set<Mission>().Remove(mission);

            _dbContext.Set<MissionImage>()
                .RemoveRange(_dbContext.Set<MissionImage>().Where(x => x.MissionId == mission.Id));
            _dbContext.Set<MissionNote>()
                .RemoveRange(_dbContext.Set<MissionNote>().Where(x => x.MissionId == mission.Id));
            _dbContext.Set<MissionDocument>()
                .RemoveRange(_dbContext.Set<MissionDocument>().Where(x => x.MissionId == mission.Id));

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
