using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class EmployerSyncQuery : DbSyncQuery<EmployerDto>
    {
    }
    public class EmployerSyncQueryHandler : BaseDbSyncHandler<EmployerSyncQuery, Employer, EmployerDto>
    {
        public EmployerSyncQueryHandler(IAppDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper, false)
        { }
    }
}
