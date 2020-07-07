using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using System.Linq;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public class MissionSyncQuery : UserDbSyncQuery<MissionDto>
    {
    }
    public class MissionSyncQueryHandler : BaseDbSyncHandler<MissionSyncQuery, Mission, MissionDto>
    {
        public MissionSyncQueryHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true)
        { }

        protected override IQueryable<Mission> AppendQuery(IQueryable<Mission> query, MissionSyncQuery request)
        {
            if (request.User.Role == "Oppdragsgiver")//Only allow employers missions if role is employer
            {
                query = query.Where(x => x.EmployerId == request.User.EmployerId);
            }

            return query;
        }
    }
}
