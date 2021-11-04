using AutoMapper;
using AutoMapper.QueryableExtensions;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Application.Queries.DbSyncQueries.Dto;
using BjBygg.Application.Common;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class EmployerSyncQuery : DbSyncQuery, IRequest<SyncEntityResponse<SyncEmployerDto>> 
    { 
        public EmployerSyncQuery(DbSyncQueryPayload _payload): base(_payload) { }
    }
    public class EmployerSyncQueryHandler : IRequestHandler<EmployerSyncQuery, SyncEntityResponse<SyncEmployerDto>>
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

        public async Task<SyncEntityResponse<SyncEmployerDto>> Handle(EmployerSyncQuery request, CancellationToken cancellationToken)
        {
            var latestUpdate = _syncTimestamps.Timestamps[typeof(Employer)];
            if (!request.InitialSync && latestUpdate != null && latestUpdate <= request.Timestamp) return null;

            var query = _dbContext.Set<Employer>().GetSyncItems(request);
           
            if (request.User.Role == Roles.Employer)
            {
                query = query.Where(x => x.Id == request.User.EmployerId);
            }

            return await query
                 .ProjectTo<SyncEmployerQueryDto>(_mapper.ConfigurationProvider)
                 .ToSyncResponseAsync<SyncEmployerQueryDto, SyncEmployerDto>(request.InitialSync, _mapper);
        }

    }
}
