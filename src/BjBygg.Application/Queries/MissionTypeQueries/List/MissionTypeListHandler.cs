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
using BjBygg.Application.Queries.MissionTypeQueries.List;
using BjBygg.Application.Queries.MissionTypeQueries;
using BjBygg.Application.Shared;

namespace BjBygg.Application.Queries.MissionQueries.List
{
    public class MissionTypeListHandler : IRequestHandler<MissionTypeListQuery, IEnumerable<MissionTypeDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionTypeListHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MissionTypeDto>> Handle(MissionTypeListQuery request, CancellationToken cancellationToken)
        {
            var missionTypes = await _dbContext.Set<MissionType>().ToListAsync();

            return missionTypes.Select(x => _mapper.Map<MissionTypeDto>(x));
        }
    }

 
}
