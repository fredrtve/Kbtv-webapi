using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BjBygg.Application.Queries.DbSyncQueries.MissionQuery
{
    public class MissionSyncHandler : BaseDbSyncHandler<MissionSyncQuery, Mission, MissionDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public MissionSyncHandler(AppDbContext dbContext, UserManager<ApplicationUser> userManager, IMapper mapper) : 
            base(dbContext, mapper, true) {
            _userManager = userManager;
        }

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
