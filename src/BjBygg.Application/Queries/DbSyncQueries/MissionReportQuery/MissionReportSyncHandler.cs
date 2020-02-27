using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries.MissionReportQuery
{
    public class MissionReportSyncHandler : DbSyncHandler<MissionReport, MissionReportSyncQuery, MissionReportDto>
    {
        public MissionReportSyncHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
      
        }

    }
}
