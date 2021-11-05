using AutoMapper;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Core.Entities;
using System;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Dto
{
    public abstract class CommonMissionDto
    {
        public string Id { get; set; }
        public string Address { get; set; }

        public bool Finished { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Description { get; set; }

        public string EmployerId { get; set; }

        public string? FileName { get; set; }

        public Position Position { get; set; }
    }

    public class SyncMissionQueryDto : CommonMissionDto, IDbSyncQueryResponse
    {
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class SyncMissionDto : CommonMissionDto
    {
        public long CreatedAt { get; set; }
    }

    public class SyncMissionProfiles : Profile
    {
        public SyncMissionProfiles()
        {
            CreateMap<Mission, SyncMissionQueryDto>();
            CreateMap<SyncMissionQueryDto, SyncMissionDto>();
        }
    }
}
