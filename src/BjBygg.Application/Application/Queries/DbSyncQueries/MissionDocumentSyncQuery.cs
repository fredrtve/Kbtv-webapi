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
    public class MissionDocumentSyncQuery : UserDbSyncQuery<MissionDocumentDto>
    {
    }

    public class MissionDocumentSyncQueryHandler : BaseDbSyncHandler<MissionDocumentSyncQuery, MissionDocument, MissionDocumentDto>
    {
        public MissionDocumentSyncQueryHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true)
        { }

        protected override IQueryable<MissionDocument> AppendQuery(IQueryable<MissionDocument> query, MissionDocumentSyncQuery request)
        {
            if (request.User.Role == Roles.Employer) //Only allow employers missions if role is employer
                query = query.Include(x => x.Mission).Where(x => (x.Mission.EmployerId == request.User.EmployerId) && !x.Mission.Deleted);

            return query;
        }
    }
}
