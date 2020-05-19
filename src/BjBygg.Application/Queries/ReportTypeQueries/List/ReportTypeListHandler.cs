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
using BjBygg.Application.Shared;

namespace BjBygg.Application.Queries.ReportTypeQueries.List
{
    public class ReportTypeListHandler : IRequestHandler<ReportTypeListQuery, IEnumerable<ReportTypeDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReportTypeListHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportTypeDto>> Handle(ReportTypeListQuery request, CancellationToken cancellationToken)
        {
            var ReportTypes = await _dbContext.Set<ReportType>().ToListAsync();

            return ReportTypes.Select(x => _mapper.Map<ReportTypeDto>(x));
        }
    }

 
}
