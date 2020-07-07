using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using System.Linq;


namespace BjBygg.Application.Queries.DbSyncQueries
{
    public class UserTimesheetSyncQuery : UserDbSyncQuery<TimesheetDto>
    {
    }
    public class UserTimesheetSyncQueryHandler : BaseDbSyncHandler<UserTimesheetSyncQuery, Timesheet, TimesheetDto>
    {
        public UserTimesheetSyncQueryHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true)
        { }

        protected override IQueryable<Timesheet> AppendQuery(IQueryable<Timesheet> query, UserTimesheetSyncQuery request)
        {
            query = query.Where(x => x.UserName == request.User.UserName); //Only users entities

            return query;
        }
    }
}
