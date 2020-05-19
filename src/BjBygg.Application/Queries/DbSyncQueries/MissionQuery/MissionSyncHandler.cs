using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Queries.DbSyncQueries.MissionQuery
{
    public class MissionSyncHandler : IRequestHandler<MissionSyncQuery, DbSyncResponse<MissionDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionSyncHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DbSyncResponse<MissionDto>> Handle(MissionSyncQuery request, CancellationToken cancellationToken)
        {
            if (request.User == null) throw new EntityNotFoundException($"No user found");

            List<Mission> entities;
            List<int> deletedEntities = new List<int>();

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var date = dtDateTime.AddSeconds(request.Timestamp ?? 0);
            var minDate = DateTime.Now.AddYears(-5);

            IQueryable<Mission> query = _dbContext.Set<Mission>();

            Boolean initialCall = (DateTime.Compare(date, minDate) < 0); //If last updated resource is older than 5 years

            if (initialCall)
                query = query.Where(x => DateTime.Compare(x.CreatedAt, minDate) > 0);
            else
            {
                query = query.IgnoreQueryFilters(); //Include deleted property to check for deleted entities
                query = query.Where(x => DateTime.Compare(x.UpdatedAt, date) > 0);
            }

            if (request.User.Role == "Oppdragsgiver") //Only allow employers missions if role is employer
                query = query.Where(x => x.EmployerId == request.User.EmployerId);
            
            entities = await query.ToListAsync();

            if (!initialCall)
            {
                deletedEntities = entities.Where(x => x.Deleted == true).Select(x => x.Id).ToList(); //Add ids from deleted entities
                entities = entities.Where(x => x.Deleted == false).ToList(); //Remove deleted entities
            }

            return new DbSyncResponse<MissionDto>()
            {
                Entities = _mapper.Map<IEnumerable<MissionDto>>(entities),
                DeletedEntities = deletedEntities,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            };
        }

    }
}
