using AutoMapper;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Queries.DbSyncQueries
{
    public class EmployerSyncQuery : DbSyncQuery<EmployerDto>
    {
    }
    public class EmployerSyncQueryHandler : BaseDbSyncHandler<EmployerSyncQuery, Employer, EmployerDto>
    {
        public EmployerSyncQueryHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, false)
        { }
    }
}
