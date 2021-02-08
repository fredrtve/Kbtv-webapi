using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class EmployerSyncQuery : UserDbSyncQuery<EmployerDto>
    {
    }
    public class EmployerSyncQueryHandler : IRequestHandler<EmployerSyncQuery, DbSyncArrayResponse<EmployerDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployerSyncQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<DbSyncArrayResponse<EmployerDto>> Handle(EmployerSyncQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Employer>().AsQueryable().GetSyncItems(request, false);
   
            if (request.User.Role == Roles.Employer)
            {
                query = query.Where(x => x.Id == request.User.EmployerId);
            }

            return await query.ToSyncArrayResponseAsync<Employer, EmployerDto>(request.Timestamp == null, _mapper);
        }

    }
}
