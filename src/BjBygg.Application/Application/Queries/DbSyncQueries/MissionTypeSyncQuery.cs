using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class MissionTypeSyncQuery : DbSyncQuery, IRequest<DbSyncArrayResponse<MissionTypeDto>> { }
    public class MissionTypeSyncQueryHandler : IRequestHandler<MissionTypeSyncQuery, DbSyncArrayResponse<MissionTypeDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncTimestamps _syncTimestamps;

        public MissionTypeSyncQueryHandler(IAppDbContext dbContext, IMapper mapper, ISyncTimestamps syncTimestamps)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncTimestamps = syncTimestamps;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<DbSyncArrayResponse<MissionTypeDto>> Handle(MissionTypeSyncQuery request, CancellationToken cancellationToken)
        {
            var latestUpdate = _syncTimestamps.Timestamps[typeof(MissionType)];
            if (!request.InitialSync && latestUpdate != null && latestUpdate <= request.Timestamp) return null;

            return (await _dbContext.Set<MissionType>().AsQueryable()
                .GetSyncItems(request, true).ToListAsync())
                .ToSyncArrayResponse<MissionType, MissionTypeDto>(request.InitialSync, _mapper);
        }
    }
}
