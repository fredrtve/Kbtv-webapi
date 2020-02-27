using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.DbSyncQueries.MissionNoteQuery
{
    public class MissionNoteSyncHandler : DbSyncHandler<MissionNote, MissionNoteSyncQuery, MissionNoteDto>
    {
        public MissionNoteSyncHandler(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
      
        }

    }
}
