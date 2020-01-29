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

namespace BjBygg.Application.Queries.MissionReportTypeQueries.List
{
    public class MissionReportTypeListHandler : IRequestHandler<MissionReportTypeListQuery, IEnumerable<MissionReportTypeDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionReportTypeListHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MissionReportTypeDto>> Handle(MissionReportTypeListQuery request, CancellationToken cancellationToken)
        {
            var missionReportTypes = await _dbContext.Set<MissionReportType>().ToListAsync();

            return missionReportTypes.Select(x => _mapper.Map<MissionReportTypeDto>(x));
        }
    }

 
}
