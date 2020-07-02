using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using System.Linq;

namespace BjBygg.Application.Queries.DbSyncQueries.MissionQuery
{
    public class MissionSyncHandler : BaseDbSyncHandler<MissionSyncQuery, Mission, MissionDto>
    {
        public MissionSyncHandler(AppDbContext dbContext, IMapper mapper) : 
            base(dbContext, mapper, true) {}

        protected override IQueryable<Mission> AppendQuery(IQueryable<Mission> query, MissionSyncQuery request)
        {
            if (request.User == null) throw new EntityNotFoundException($"No user found");

            if (request.User.Role == "Oppdragsgiver") //Only allow employers missions if role is employer
                query = query.Where(x => x.EmployerId == request.User.EmployerId);

            return query;
        }
    }
}
