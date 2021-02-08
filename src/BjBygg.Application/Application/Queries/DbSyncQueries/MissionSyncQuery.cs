using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class MissionSyncQuery : UserDbSyncQuery<MissionDto>{}

    public class MissionSyncQueryHandler : IRequestHandler<MissionSyncQuery, DbSyncArrayResponse<MissionDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionSyncQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<DbSyncArrayResponse<MissionDto>> Handle(MissionSyncQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Mission>().AsQueryable().GetSyncItems(request);

            if (request.User.Role == Roles.Employer) //Only allow employers missions if role is employer
            {
                query = query.Where(x => x.EmployerId == request.User.EmployerId); 
            }

            return await query.ToSyncArrayResponseAsync<Mission, MissionDto>(request.Timestamp == null, _mapper);
        }

    }
}
