using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using MediatR;
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
            _dbContext.Set<Mission>().RemoveRange(
                _dbContext.Set<Mission>().Where(x => request.Ids.Contains(x.Id)).ToList());

            _dbContext.Set<MissionImage>().RemoveRange(
                _dbContext.Set<MissionImage>().Where(x => request.Ids.Contains(x.MissionId)));

            _dbContext.Set<MissionNote>().RemoveRange(
                _dbContext.Set<MissionNote>().Where(x => request.Ids.Contains(x.MissionId)));

            _dbContext.Set<MissionDocument>().RemoveRange(
                _dbContext.Set<MissionDocument>().Where(x => request.Ids.Contains(x.MissionId)));

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
