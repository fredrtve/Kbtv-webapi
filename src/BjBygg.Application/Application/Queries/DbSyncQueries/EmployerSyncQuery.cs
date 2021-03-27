using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class EmployerSyncQuery : UserDbSyncQuery, IRequest<DbSyncArrayResponse<EmployerDto>> { }
    public class EmployerSyncQueryHandler : IRequestHandler<EmployerSyncQuery, DbSyncArrayResponse<EmployerDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncTimestamps _syncTimestamps;

        public EmployerSyncQueryHandler(IAppDbContext dbContext, IMapper mapper, ISyncTimestamps syncTimestamps)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncTimestamps = syncTimestamps;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<DbSyncArrayResponse<EmployerDto>> Handle(EmployerSyncQuery request, CancellationToken cancellationToken)
        {
            var latestUpdate = _syncTimestamps.Timestamps[typeof(Employer)];
            if (!request.InitialSync && latestUpdate != null && latestUpdate <= request.Timestamp) return null;

            var query = _dbContext.Set<Employer>().AsQueryable().GetSyncItems(request, true);

            if (request.User.Role == Roles.Employer)
            {
                query = query.Where(x => x.Id == request.User.EmployerId);
            }

            return (await query.ToListAsync())
                .ToSyncArrayResponse<Employer, EmployerDto>(request.InitialSync, _mapper);
        }

    }
}
