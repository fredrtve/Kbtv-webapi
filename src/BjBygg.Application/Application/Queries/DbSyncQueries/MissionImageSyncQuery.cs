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
    public class MissionImageSyncQuery : UserDbSyncQuery<MissionImageDto>{}
    public class MissionImageSyncQueryHandler : IRequestHandler<MissionImageSyncQuery, DbSyncArrayResponse<MissionImageDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionImageSyncQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<DbSyncArrayResponse<MissionImageDto>> Handle(MissionImageSyncQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<MissionImage>().AsQueryable()
                .GetMissionChildSyncItems(request)
                .ToSyncArrayResponseAsync<MissionImage, MissionImageDto>(request.Timestamp == null, _mapper);
        }
    }
}
