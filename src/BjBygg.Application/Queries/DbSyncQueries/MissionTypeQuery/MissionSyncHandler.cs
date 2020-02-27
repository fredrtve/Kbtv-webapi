using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries.MissionTypeQuery
{
    public class MissionTypeSyncHandler : DbSyncHandler<MissionType, MissionTypeSyncQuery, MissionTypeDto>
    {
        public MissionTypeSyncHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
      
        }

    }
}
