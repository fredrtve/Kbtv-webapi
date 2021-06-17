using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class UserTimesheetSyncQuery : DbSyncQuery, IRequest<DbSyncArrayResponse<UserTimesheetDto>> { }
    public class UserTimesheetSyncQueryHandler : IRequestHandler<UserTimesheetSyncQuery, DbSyncArrayResponse<UserTimesheetDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncTimestamps _syncTimestamps;
        private readonly ICurrentUserService _currentUserService;

        public UserTimesheetSyncQueryHandler(IAppDbContext dbContext, IMapper mapper, ISyncTimestamps syncTimestamps, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
           _syncTimestamps = syncTimestamps;
            _currentUserService = currentUserService;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<DbSyncArrayResponse<UserTimesheetDto>> Handle(UserTimesheetSyncQuery request, CancellationToken cancellationToken)
        {
            var latestUpdate = _syncTimestamps.Timestamps[typeof(Timesheet)];
            if (!request.InitialSync && latestUpdate != null && latestUpdate <= request.Timestamp) return null;

            var query = _dbContext.Set<Timesheet>().AsQueryable().GetSyncItems(request);

            query = query.Where(x => x.UserName == _currentUserService.UserName); //Only users entities

            return (await query.ToListAsync())
                .ToSyncArrayResponse<Timesheet, UserTimesheetDto>(request.InitialSync, _mapper);
        }
    }
}
