using AutoMapper;
using AutoMapper.QueryableExtensions;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Application.Queries.DbSyncQueries.Dto;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class UserTimesheetSyncQuery : DbSyncQuery, IRequest<SyncEntityResponse<SyncUserTimesheetDto>>
    {
        public UserTimesheetSyncQuery(DbSyncQueryPayload _payload) : base(_payload) { }
    }
    public class UserTimesheetSyncQueryHandler : IRequestHandler<UserTimesheetSyncQuery, SyncEntityResponse<SyncUserTimesheetDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncTimestamps _syncTimestamps;

        public UserTimesheetSyncQueryHandler(IAppDbContext dbContext, IMapper mapper, ISyncTimestamps syncTimestamps)
        {
            _dbContext = dbContext;
            _mapper = mapper;
           _syncTimestamps = syncTimestamps;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<SyncEntityResponse<SyncUserTimesheetDto>> Handle(UserTimesheetSyncQuery request, CancellationToken cancellationToken)
        {
            var latestUpdate = _syncTimestamps.Timestamps[typeof(Timesheet)];
            if (!request.InitialSync && latestUpdate != null && latestUpdate <= request.Timestamp) return null;

            var query = _dbContext.Set<Timesheet>().GetSyncItems(request);

            query = query.Where(x => x.UserName == request.User.UserName); //Only users entities

            return await query
                .ProjectTo<SyncUserTimesheetQueryDto>(_mapper.ConfigurationProvider)
                .ToSyncResponseAsync<SyncUserTimesheetQueryDto, SyncUserTimesheetDto>(request.InitialSync, _mapper);
        }
    }
}
