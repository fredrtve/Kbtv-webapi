using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries.MissionQuery
{
    public class MissionSyncHandler : DbSyncHandler<Mission, MissionSyncQuery, MissionDto>
    {
        public MissionSyncHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
      
        }

    }
}
