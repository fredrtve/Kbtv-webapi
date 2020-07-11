using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.TimesheetQueries
{
    public class TimesheetQueryHandler : IRequestHandler<TimesheetQuery, IEnumerable<TimesheetDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public TimesheetQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IEnumerable<TimesheetDto>> Handle(TimesheetQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Timesheet>().AsQueryable();

            if (request.MissionId != null)
                query = query.Where(x => x.MissionId == request.MissionId);

            if (request.UserName != null)
                query = query.Where(x => x.UserName == request.UserName);

            var startDate = DateTimeHelper.ConvertEpochToDate(request.StartDate ?? 0);

            if (request.EndDate == null)
                query = query.Where(x => x.StartTime.Date >= startDate.Date);
            else
            {
                var endDate = DateTimeHelper.ConvertEpochToDate(request.EndDate ?? 0);
                query = query.Where(x => x.StartTime.Date >= startDate.Date && x.StartTime.Date <= endDate.Date);
            }

            return _mapper.Map<IEnumerable<TimesheetDto>>(await query.ToListAsync());
        }

    }
}
