
using AutoMapper;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Dto
{
    public abstract class CommonMissionImageDto
    {
        public string Id { get; set; }
        public string MissionId { get; set; }

        public string FileName { get; set; }

    }
    public class SyncMissionImageQueryDto : CommonMissionImageDto, IDbSyncQueryResponse
    {
        public bool Deleted { get; set; }
    }

    public class SyncMissionImageDto : CommonMissionImageDto {}

    public class SyncMissionImageProfiles : Profile
    {
        public SyncMissionImageProfiles()
        {
            CreateMap<MissionImage, SyncMissionImageQueryDto>();
            CreateMap<SyncMissionImageQueryDto, SyncMissionImageDto>();
        }
    }
}
