using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public class MissionNoteSyncQuery : UserDbSyncQuery<MissionNoteDto>
    {
    }
    public class MissionNoteSyncQueryHandler : BaseDbSyncHandler<MissionNoteSyncQuery, MissionNote, MissionNoteDto>
    {
        public MissionNoteSyncQueryHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true)
        { }

        protected override IQueryable<MissionNote> AppendQuery(IQueryable<MissionNote> query, MissionNoteSyncQuery request)
        {
            if (request.User.Role == "Oppdragsgiver") //Only allow employers missions if role is employer
                query = query.Include(x => x.Mission).Where(x => x.Mission.EmployerId == request.User.EmployerId);

            return query;
        }
    }
}
