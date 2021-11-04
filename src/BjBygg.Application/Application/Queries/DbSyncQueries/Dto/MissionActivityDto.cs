using AutoMapper;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Dto
{
    public abstract class CommonSyncMissionActivityDto 
    {
        public string Id { get; set; }
        public string MissionId { get; set; }
        public string ActivityId { get; set; }
    }

    public class SyncMissionActivityDto : CommonSyncMissionActivityDto { }
    public class SyncMissionActivityQueryDto : CommonSyncMissionActivityDto, IDbSyncQueryResponse
    {
        public bool Deleted { get; set; }
    }

    public class SyncMissionActivityProfiles : Profile
    {
        public SyncMissionActivityProfiles()
        {
            CreateMap<MissionActivity, SyncMissionActivityQueryDto>();
            CreateMap<SyncMissionActivityQueryDto, SyncMissionActivityDto>();
        }
    }
}
