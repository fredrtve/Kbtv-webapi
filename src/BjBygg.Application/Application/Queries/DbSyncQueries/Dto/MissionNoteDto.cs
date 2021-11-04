using AutoMapper;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Core.Entities;
using System;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Dto
{
    public class CommonMissionNoteDto
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public string MissionId { get; set; }
    }
    public class SyncMissionNoteQueryDto : CommonMissionNoteDto, IDbSyncQueryResponse
    {
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SyncMissionNoteDto : CommonMissionNoteDto
    {
        public long CreatedAt { get; set; }
    }
    public class SyncMissionNoteProfiles : Profile
    {
        public SyncMissionNoteProfiles()
        {
            CreateMap<MissionNote, SyncMissionNoteQueryDto>();
            CreateMap<SyncMissionNoteQueryDto, SyncMissionNoteDto>();
        }
    }
}
