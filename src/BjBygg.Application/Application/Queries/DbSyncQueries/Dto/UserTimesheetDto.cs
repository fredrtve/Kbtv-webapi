using AutoMapper;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Application.Queries.DbSyncQueries.Dto;
using BjBygg.Core.Entities;
using BjBygg.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Application.Queries.DbSyncQueries.Dto
{
    public abstract class CommonUserTimesheetDto
    {
        public string Id { get; set; }

        public string MissionId { get; set; }

        public double TotalHours { get; set; }

        public TimesheetStatus Status { get; set; }

        public string Comment { get; set; }
    }
    public class SyncUserTimesheetQueryDto : CommonUserTimesheetDto, IDbSyncQueryResponse
    {
        public bool Deleted { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class SyncUserTimesheetDto : CommonUserTimesheetDto {
        public long StartTime { get; set; }
        public long EndTime { get; set; }
    }

    public class SyncUserTimesheetProfiles : Profile
    {
        public SyncUserTimesheetProfiles()
        {
            CreateMap<Timesheet, SyncUserTimesheetQueryDto>();
            CreateMap<SyncUserTimesheetQueryDto, SyncUserTimesheetDto>();
        }
    }
}
