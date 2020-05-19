using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Queries.DbSyncQueries.MissionTypeQuery
{
    public class MissionTypeSyncHandler : IRequestHandler<MissionTypeSyncQuery, DbSyncResponse<MissionTypeDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionTypeSyncHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DbSyncResponse<MissionTypeDto>> Handle(MissionTypeSyncQuery request, CancellationToken cancellationToken)
        {
            List<MissionType> entities;
            List<int> deletedEntities = new List<int>();

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var date = dtDateTime.AddSeconds(request.Timestamp ?? 0);

            IQueryable<MissionType> query = _dbContext.Set<MissionType>();

            if(request.Timestamp != null)
                query = query.IgnoreQueryFilters(); //Include deleted property to check for deleted entities

            query = query.Where(x => DateTime.Compare(x.UpdatedAt, date) > 0);
            
            entities = await query.ToListAsync();

            if (request.Timestamp != null)
            {
                deletedEntities = entities.Where(x => x.Deleted == true).Select(x => x.Id).ToList(); //Add ids from deleted entities
                entities = entities.Where(x => x.Deleted == false).ToList(); //Remove deleted entities
            }

            return new DbSyncResponse<MissionTypeDto>()
            {
                Entities = _mapper.Map<IEnumerable<MissionTypeDto>>(entities),
                DeletedEntities = deletedEntities,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            };
        }

    }
}
