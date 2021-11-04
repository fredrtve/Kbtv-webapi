using AutoMapper;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Core.Entities;
using System;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Dto
{
    public abstract class CommonMissionDocumentDto
    { 
        public string Id { get; set; }
        public string MissionId { get; set; }

        public string FileName { get; set; }

        public string Name { get; set; }
    }

    public class SyncMissionDocumentQueryDto : CommonMissionDocumentDto, IDbSyncQueryResponse
    {
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SyncMissionDocumentDto : CommonMissionDocumentDto
    {
        public long CreatedAt { get; set; }
    }

    public class SyncMissionDocumentProfiles : Profile
    {
        public SyncMissionDocumentProfiles()
        {
            CreateMap<MissionDocument, SyncMissionDocumentQueryDto>();
            CreateMap<SyncMissionDocumentQueryDto, SyncMissionDocumentDto>();
        }
    }
}
