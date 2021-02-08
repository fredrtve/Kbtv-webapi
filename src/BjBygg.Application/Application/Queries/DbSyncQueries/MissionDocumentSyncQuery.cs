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
    public class MissionDocumentSyncQuery : UserDbSyncQuery<MissionDocumentDto> {}

    public class MissionDocumentSyncQueryHandler : IRequestHandler<MissionDocumentSyncQuery, DbSyncArrayResponse<MissionDocumentDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionDocumentSyncQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<DbSyncArrayResponse<MissionDocumentDto>> Handle(MissionDocumentSyncQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<MissionDocument>().AsQueryable()
                .GetMissionChildSyncItems(request)
                .ToSyncArrayResponseAsync<MissionDocument, MissionDocumentDto>(request.Timestamp == null, _mapper);
        }

    }
}
