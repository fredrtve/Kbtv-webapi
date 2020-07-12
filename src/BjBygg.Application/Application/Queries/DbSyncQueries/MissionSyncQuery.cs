using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using System.Linq;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class MissionSyncQuery : UserDbSyncQuery<MissionDto>
    {
    }
    public class MissionSyncQueryHandler : BaseDbSyncHandler<MissionSyncQuery, Mission, MissionDto>
    {
        public MissionSyncQueryHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true)
        { }

        protected override IQueryable<Mission> AppendQuery(IQueryable<Mission> query, MissionSyncQuery request)
        {
            if (request.User.Role == Roles.Employer)//Only allow employers missions if role is employer
            {
                query = query.Where(x => x.EmployerId == request.User.EmployerId);
            }

            return query;
        }
    }
}
