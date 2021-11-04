using AutoMapper;
using AutoMapper.QueryableExtensions;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Application.Queries.DbSyncQueries.Dto;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class ActivitySyncQuery : DbSyncQuery, IRequest<SyncEntityResponse<SyncActivityDto>>
    {
        public ActivitySyncQuery(DbSyncQueryPayload _payload) : base(_payload) { }
    }
    public class ActivitySyncQueryHandler : IRequestHandler<ActivitySyncQuery, SyncEntityResponse<SyncActivityDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncTimestamps _syncTimestamps;

        public ActivitySyncQueryHandler(IAppDbContext dbContext, IMapper mapper, ISyncTimestamps syncTimestamps)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncTimestamps = syncTimestamps;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<SyncEntityResponse<SyncActivityDto>> Handle(ActivitySyncQuery request, CancellationToken cancellationToken)
        {
            var latestUpdate = _syncTimestamps.Timestamps[typeof(Activity)];
            if (!request.InitialSync && latestUpdate != null && latestUpdate <= request.Timestamp) return null;

            return await _dbContext.Set<Activity>()
                .GetSyncItems(request, true)
                .ProjectTo<SyncActivityQueryDto>(_mapper.ConfigurationProvider)
                .ToSyncResponseAsync<SyncActivityQueryDto, SyncActivityDto>(request.InitialSync, _mapper);
        }
    }
}
