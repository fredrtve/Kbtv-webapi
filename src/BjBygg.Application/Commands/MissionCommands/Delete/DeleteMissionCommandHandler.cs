using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Delete
{
    public class DeleteMissionCommandHandler : IRequestHandler<DeleteMissionCommand>
    {
        private readonly AppDbContext _dbContext;

        public DeleteMissionCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = await _dbContext.Set<Mission>().FindAsync(request.Id);

            if (mission == null) throw new EntityNotFoundException(nameof(Mission), request.Id);

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
