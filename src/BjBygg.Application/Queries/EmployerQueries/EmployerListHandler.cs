using CleanArchitecture.Infrastructure.Data;
using MediatR;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BjBygg.Application.Common;

namespace BjBygg.Application.Queries.EmployerQueries.List
{
    public class EmployerListHandler : IRequestHandler<EmployerListQuery, IEnumerable<EmployerDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployerListHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IEnumerable<EmployerDto>> Handle(EmployerListQuery request, CancellationToken cancellationToken)
        {
            var employers = await _dbContext.Set<Employer>().ToListAsync();

            return employers.Select(x => _mapper.Map<EmployerDto>(x));
        }
    }

 
}
