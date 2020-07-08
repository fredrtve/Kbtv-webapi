using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.DeleteRange
{
    public class DeleteRangeMissionCommandHandler : IRequestHandler<DeleteRangeMissionCommand>
    {
        private readonly AppDbContext _dbContext;

        public DeleteRangeMissionCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteRangeMissionCommand request, CancellationToken cancellationToken)
        {
            var entities = _dbContext.Set<Mission>().Where(x => request.Ids.Contains(x.Id)).ToList();

            if (entities.Count() == 0)
                throw new EntityNotFoundException(nameof(Mission), String.Join(", ", request.Ids.ToArray()));

            _dbContext.Set<Mission>().RemoveRange(entities);

            _dbContext.Set<MissionImage>()
                .RemoveRange(_dbContext.Set<MissionImage>().Where(x => request.Ids.Contains(x.MissionId)));
            _dbContext.Set<MissionNote>()
                .RemoveRange(_dbContext.Set<MissionNote>().Where(x => request.Ids.Contains(x.MissionId)));
            _dbContext.Set<MissionDocument>()
                .RemoveRange(_dbContext.Set<MissionDocument>().Where(x => request.Ids.Contains(x.MissionId)));

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
