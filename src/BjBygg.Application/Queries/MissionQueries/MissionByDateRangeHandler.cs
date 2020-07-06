using CleanArchitecture.Infrastructure.Data;
using MediatR;
using CleanArchitecture.Core.Entities;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BjBygg.Application.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CleanArchitecture.Core.Exceptions;
using System.Collections.Generic;

namespace BjBygg.Application.Queries.MissionQueries
{
    public class MissionByDateRangeHandler : IRequestHandler<MissionByDateRangeQuery, IEnumerable<MissionDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionByDateRangeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IEnumerable<MissionDto>> Handle(MissionByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var missions = await _dbContext.Set<Mission>()
                .Where(x => x.CreatedAt > request.FromDate && x.CreatedAt <= request.ToDate)
                .OrderByDescending(x => x.CreatedAt)
                .Include(x => x.Employer)
                .Include(x => x.MissionType)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MissionDto>>(missions);
        }
    }
}
