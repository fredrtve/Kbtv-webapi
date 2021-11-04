using AutoMapper;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Application.Queries.DbSyncQueries.Dto;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Dto
{
    public abstract class CommonMissionTypeDto
    {
        public string? Id { get; set; }

        public string? Name { get; set; }
    }
    public class SyncMissionTypeQueryDto : CommonMissionTypeDto, IDbSyncQueryResponse
    {
        public bool Deleted { get; set; }
    }

    public class SyncMissionTypeDto : CommonMissionTypeDto { }

    public class SyncMissionTypeProfiles : Profile
    {
        public SyncMissionTypeProfiles()
        {
            CreateMap<MissionType, SyncMissionTypeQueryDto>();
            CreateMap<SyncMissionTypeQueryDto, SyncMissionTypeDto>();
        }
    }
}
