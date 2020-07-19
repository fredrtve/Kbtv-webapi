using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using System.Linq;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class EmployerSyncQuery : UserDbSyncQuery<EmployerDto>
    {
    }
    public class EmployerSyncQueryHandler : BaseDbSyncHandler<EmployerSyncQuery, Employer, EmployerDto>
    {
        public EmployerSyncQueryHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, false)
        { }

        protected override IQueryable<Employer> AppendQuery(IQueryable<Employer> query, EmployerSyncQuery request)
        {
            if (request.User.Role == Roles.Employer)
            {
                query = query.Where(x => x.Id == request.User.EmployerId);
            }

            return query;
        }
    }
}
