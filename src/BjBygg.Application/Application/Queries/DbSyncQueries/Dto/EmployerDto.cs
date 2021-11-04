using AutoMapper;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Application.Queries.DbSyncQueries.Dto;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Dto
{
    public abstract class CommonEmployerDto
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

    }
    public class SyncEmployerQueryDto : CommonEmployerDto, IDbSyncQueryResponse
    {
        public bool Deleted { get; set; }
    }

    public class SyncEmployerDto : CommonEmployerDto {}

    public class SyncEmployerProfiles : Profile
    {
        public SyncEmployerProfiles()
        {
            CreateMap<Employer, SyncEmployerQueryDto>();
            CreateMap<SyncEmployerQueryDto, SyncEmployerDto>();
        }
    }
}
