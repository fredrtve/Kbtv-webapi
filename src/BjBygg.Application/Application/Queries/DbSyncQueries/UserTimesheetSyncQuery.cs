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
    public class UserTimesheetSyncQuery : UserDbSyncQuery, IRequest<DbSyncArrayResponse<TimesheetDto>> { }
    public class UserTimesheetSyncQueryHandler : IRequestHandler<UserTimesheetSyncQuery, DbSyncArrayResponse<TimesheetDto>>
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
        public async Task<DbSyncArrayResponse<TimesheetDto>> Handle(UserTimesheetSyncQuery request, CancellationToken cancellationToken)
        {
            var latestUpdate = _syncTimestamps.Timestamps[typeof(Timesheet)];
            if (!request.InitialSync && latestUpdate != null && latestUpdate <= request.Timestamp) return null;

            var query = _dbContext.Set<Timesheet>().AsQueryable().GetSyncItems(request);

            query = query.Where(x => x.UserName == request.User.UserName); //Only users entities

            return (await query.ToListAsync())
                .ToSyncArrayResponse<Timesheet, TimesheetDto>(request.InitialSync, _mapper);
        }
    }
}
