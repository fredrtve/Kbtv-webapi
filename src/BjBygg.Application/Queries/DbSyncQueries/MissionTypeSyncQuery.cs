using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public class MissionTypeSyncQuery : DbSyncQuery<MissionTypeDto>
    {
    }
    public class MissionTypeSyncQueryHandler : BaseDbSyncHandler<MissionTypeSyncQuery, MissionType, MissionTypeDto>
    {
        public MissionTypeSyncQueryHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, false)
        { }
    }
}
