using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;

namespace BjBygg.Application.Commands.MissionCommands.DeleteRange
{
    public class DeleteRangeMissionHandler : IRequestHandler<DeleteRangeMissionCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteRangeMissionHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteRangeMissionCommand request, CancellationToken cancellationToken)
        {
            var entities = _dbContext.Set<Mission>().Where(x => request.Ids.Contains(x.Id)).ToList();

            if (entities.Count() == 0) throw new EntityNotFoundException($"No entities found with given id's");

            _dbContext.Set<Mission>().RemoveRange(entities);

            _dbContext.Set<MissionImage>()
                .RemoveRange(_dbContext.Set<MissionImage>().Where(x => request.Ids.Contains(x.MissionId)));
            _dbContext.Set<MissionNote>()
                .RemoveRange(_dbContext.Set<MissionNote>().Where(x => request.Ids.Contains(x.MissionId)));
            _dbContext.Set<MissionDocument>()
                .RemoveRange(_dbContext.Set<MissionDocument>().Where(x => request.Ids.Contains(x.MissionId)));

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
