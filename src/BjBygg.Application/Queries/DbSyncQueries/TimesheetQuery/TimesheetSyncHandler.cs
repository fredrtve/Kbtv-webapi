using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Queries.DbSyncQueries.TimesheetQuery
{
    public class TimesheetSyncHandler : IRequestHandler<TimesheetSyncQuery, DbSyncResponse<TimesheetDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public TimesheetSyncHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DbSyncResponse<TimesheetDto>> Handle(TimesheetSyncQuery request, CancellationToken cancellationToken)
        {
            List<Timesheet> entities;
            List<int> deletedEntities = new List<int>();

            var minDate = DateTime.Now.AddYears(-5);
            var date = DateTime.MinValue;
            DateTime.TryParseExact(request.FromDate, "o", null, System.Globalization.DateTimeStyles.None, out date);

            IQueryable<Timesheet> query = _dbContext.Set<Timesheet>();

            //Only include self timesheets for roles other than leader, dont include open timesheets for leader
            if (request.Role != "Leder")
                query = query.Where(x => x.UserName == request.UserName);
            else
                query = query.Where(x => x.Status != TimesheetStatus.Open || x.UserName == request.UserName);

            Boolean initialCall = (DateTime.Compare(date, minDate) < 0); //If last updated resource is older than 5 years

            if (initialCall)
                query = query.Where(x => DateTime.Compare(x.CreatedAt, minDate) > 0);
            else
            {
                query = query.IgnoreQueryFilters(); //Include deleted property to check for deleted entities
                query = query.Where(x => DateTime.Compare(x.UpdatedAt, date) > 0);
            }

            entities = await query.ToListAsync();

            if (!initialCall)
            {
                deletedEntities = entities.Where(x => x.Deleted == true).Select(x => x.Id).ToList(); //Add ids from deleted entities
                entities = entities.Where(x => x.Deleted == false).ToList(); //Remove deleted entities
            }

            return new DbSyncResponse<TimesheetDto>()
            {
                Entities = _mapper.Map<IEnumerable<TimesheetDto>>(entities),
                DeletedEntities = deletedEntities,
                Timestamp = DateTime.Now.ToString("o"),
            };
        }
    }
}
