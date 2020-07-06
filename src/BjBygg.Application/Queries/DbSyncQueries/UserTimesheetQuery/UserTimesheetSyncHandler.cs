using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using System.Linq;

namespace BjBygg.Application.Queries.DbSyncQueries.TimesheetQuery
{
    public class UserTimesheetSyncHandler : BaseDbSyncHandler<UserTimesheetSyncQuery, Timesheet, TimesheetDto>
    {
        public UserTimesheetSyncHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true) {}

        protected override IQueryable<Timesheet> AppendQuery(IQueryable<Timesheet> query, UserTimesheetSyncQuery request)
        {
            query = query.Where(x => x.UserName == request.User.UserName); //Only users entities

            return query;
        }
    }
}