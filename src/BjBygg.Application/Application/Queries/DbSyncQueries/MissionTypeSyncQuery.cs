using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class MissionTypeSyncQuery : DbSyncQuery<MissionTypeDto>
    {
    }
    public class MissionTypeSyncQueryHandler : BaseDbSyncHandler<MissionTypeSyncQuery, MissionType, MissionTypeDto>
    {
        public MissionTypeSyncQueryHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, false)
        { }
    }
}
