using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BjBygg.Application.Queries.DbSyncQueries.MissionImageQuery
{
    public class MissionImageSyncHandler : BaseDbSyncHandler<MissionImageSyncQuery, MissionImage, MissionImageDto>
    {
        public MissionImageSyncHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true){}

        protected override IQueryable<MissionImage> AppendQuery(IQueryable<MissionImage> query, MissionImageSyncQuery request)
        {
            if (request.User == null) throw new EntityNotFoundException($"No user provided");

            if (request.User.Role == "Oppdragsgiver") //Only allow employers missions if role is employer
                query = query.Include(x => x.Mission).Where(x => x.Mission.EmployerId == request.User.EmployerId);

            return query;
        }
    }
}
