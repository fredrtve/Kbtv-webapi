using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;

namespace BjBygg.Application.Queries.DbSyncQueries.EmployerQuery
{
    public class EmployerSyncHandler : BaseDbSyncHandler<EmployerSyncQuery, Employer, EmployerDto>
    {
        public EmployerSyncHandler(AppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, false){}
    }
}
