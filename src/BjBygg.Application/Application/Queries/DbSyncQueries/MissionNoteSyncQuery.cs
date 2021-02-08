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
    public class MissionNoteSyncQuery : UserDbSyncQuery<MissionNoteDto>{}
    public class MissionNoteSyncQueryHandler : IRequestHandler<MissionNoteSyncQuery, DbSyncArrayResponse<MissionNoteDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionNoteSyncQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<DbSyncArrayResponse<MissionNoteDto>> Handle(MissionNoteSyncQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<MissionNote>().AsQueryable()
                .GetMissionChildSyncItems(request)
                .ToSyncArrayResponseAsync<MissionNote, MissionNoteDto>(request.Timestamp == null, _mapper);
        }
    }
}
