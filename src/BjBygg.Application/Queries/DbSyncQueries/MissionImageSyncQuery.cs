using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public class MissionImageSyncQuery : UserDbSyncQuery<MissionImageDto>
    {
    }
    public class MissionImageSyncQueryHandler : BaseDbSyncHandler<MissionImageSyncQuery, MissionImage, MissionImageDto>
    {
        public MissionImageSyncQueryHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true)
        { }

        protected override IQueryable<MissionImage> AppendQuery(IQueryable<MissionImage> query, MissionImageSyncQuery request)
        {
            if (request.User.Role == "Oppdragsgiver") //Only allow employers missions if role is employer
                query = query.Include(x => x.Mission).Where(x => x.Mission.EmployerId == request.User.EmployerId);

            return query;
        }
    }
}
