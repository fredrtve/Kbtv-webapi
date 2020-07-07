using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public class MissionDocumentSyncQuery : UserDbSyncQuery<MissionDocumentDto>
    {
    }

    public class MissionDocumentSyncQueryHandler : BaseDbSyncHandler<MissionDocumentSyncQuery, MissionDocument, MissionDocumentDto>
    {
        public MissionDocumentSyncQueryHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true)
        { }

        protected override IQueryable<MissionDocument> AppendQuery(IQueryable<MissionDocument> query, MissionDocumentSyncQuery request)
        {
            if (request.User == null) throw new EntityNotFoundException($"No user provided");

            if (request.User.Role == "Oppdragsgiver") //Only allow employers missions if role is employer
                query = query.Include(x => x.Mission).Where(x => x.Mission.EmployerId == request.User.EmployerId);

            return query;
        }
    }
}
