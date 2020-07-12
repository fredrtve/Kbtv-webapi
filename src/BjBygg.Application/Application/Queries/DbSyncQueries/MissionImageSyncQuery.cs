using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common;
using BjBygg.Application.Identity.Common;
using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class MissionImageSyncQuery : UserDbSyncQuery<MissionImageDto>
    {
    }
    public class MissionImageSyncQueryHandler : BaseDbSyncHandler<MissionImageSyncQuery, MissionImage, MissionImageDto>
    {
        public MissionImageSyncQueryHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true)
        { }

        protected override IQueryable<MissionImage> AppendQuery(IQueryable<MissionImage> query, MissionImageSyncQuery request)
        {
            if (request.User.Role == Roles.Employer) //Only allow employers missions if role is employer
                query = query.Include(x => x.Mission).Where(x => x.Mission.EmployerId == request.User.EmployerId);

            return query;
        }
    }
}
