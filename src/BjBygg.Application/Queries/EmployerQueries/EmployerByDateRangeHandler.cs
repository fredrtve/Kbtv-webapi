using CleanArchitecture.Infrastructure.Data;
using MediatR;
using CleanArchitecture.Core.Entities;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BjBygg.Application.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CleanArchitecture.Core.Exceptions;
using System.Collections.Generic;

namespace BjBygg.Application.Queries.EmployerQueries
{
    public class EmployerByDateRangeHandler : IRequestHandler<EmployerByDateRangeQuery, IEnumerable<EmployerDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployerByDateRangeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployerDto>> Handle(EmployerByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var missions = await _dbContext.Set<Employer>()
                .Where(x => x.CreatedAt > request.FromDate && x.CreatedAt <= request.ToDate)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EmployerDto>>(missions);
        }
    }
}
