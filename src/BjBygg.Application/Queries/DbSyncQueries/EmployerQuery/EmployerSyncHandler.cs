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

namespace BjBygg.Application.Queries.DbSyncQueries.EmployerQuery
{
    public class EmployerSyncHandler : IRequestHandler<EmployerSyncQuery, DbSyncResponse<EmployerDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployerSyncHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<DbSyncResponse<EmployerDto>> Handle(EmployerSyncQuery request, CancellationToken cancellationToken)
        {
            List<Employer> entities;
            List<int> deletedEntities = new List<int>();

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var date = dtDateTime.AddSeconds(request.Timestamp ?? 0);

            IQueryable<Employer> query = _dbContext.Set<Employer>();

            if (request.Timestamp != null)
                query = query.IgnoreQueryFilters(); //Include deleted property to check for deleted entities

            query = query.Where(x => DateTime.Compare(x.UpdatedAt, date) > 0);

            entities = await query.ToListAsync();

            if (request.Timestamp != null)
            {
                deletedEntities = entities.Where(x => x.Deleted == true).Select(x => x.Id).ToList(); //Add ids from deleted entities
                entities = entities.Where(x => x.Deleted == false).ToList(); //Remove deleted entities
            }

            return new DbSyncResponse<EmployerDto>()
            {
                Entities = _mapper.Map<IEnumerable<EmployerDto>>(entities),
                DeletedEntities = deletedEntities,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            };
        }

    }
}
