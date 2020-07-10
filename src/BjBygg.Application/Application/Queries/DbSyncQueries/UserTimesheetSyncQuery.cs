using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;
using System.Linq;


namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class UserTimesheetSyncQuery : UserDbSyncQuery<TimesheetDto>
    {
    }
    public class UserTimesheetSyncQueryHandler : BaseDbSyncHandler<UserTimesheetSyncQuery, Timesheet, TimesheetDto>
    {
        public UserTimesheetSyncQueryHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, true)
        { }

        protected override IQueryable<Timesheet> AppendQuery(IQueryable<Timesheet> query, UserTimesheetSyncQuery request)
        {
            query = query.Where(x => x.UserName == request.User.UserName); //Only users entities

            return query;
        }
    }
}
