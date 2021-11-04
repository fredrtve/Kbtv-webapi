using AutoMapper;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Dto
{
    public abstract class CommonSyncActivityDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class SyncActivityDto : CommonSyncActivityDto { }

    public class SyncActivityQueryDto : CommonSyncActivityDto, IDbSyncQueryResponse
    {
        public bool Deleted { get; set; }
    }

    public class SyncActivityProfiles : Profile
    {
        public SyncActivityProfiles()
        {
            CreateMap<Activity, SyncActivityQueryDto>();
            CreateMap<SyncActivityQueryDto, SyncActivityDto>();
        }
    }
}
