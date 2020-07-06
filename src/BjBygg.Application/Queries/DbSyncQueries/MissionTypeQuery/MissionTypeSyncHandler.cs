using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Queries.DbSyncQueries.MissionTypeQuery
{
    public class MissionTypeSyncHandler : BaseDbSyncHandler<MissionTypeSyncQuery, MissionType, MissionTypeDto>
    {
        public MissionTypeSyncHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, false){}
    }
}
