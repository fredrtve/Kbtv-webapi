using AutoMapper;
using AutoMapper.QueryableExtensions;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Application.Queries.DbSyncQueries.Dto;
using BjBygg.Application.Common;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class MissionSyncQuery : DbSyncQuery, IRequest<SyncMissionResponse>
    {
        public MissionSyncQuery(DbSyncQueryPayload _payload) : base(_payload) { }
    }

    public class MissionSyncQueryHandler : IRequestHandler<MissionSyncQuery, SyncMissionResponse>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISyncTimestamps _syncTimestamps;

        public MissionSyncQueryHandler(IAppDbContext dbContext, IMapper mapper, ISyncTimestamps syncTimestamps)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _syncTimestamps = syncTimestamps;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<SyncMissionResponse> Handle(MissionSyncQuery request, CancellationToken cancellationToken)
        {      
            var latestUpdate = _syncTimestamps.Timestamps[typeof(Mission)];

            if (!request.InitialSync && latestUpdate != null && latestUpdate <= request.Timestamp) return null;

            var query = _dbContext.Set<Mission>().GetSyncItems(request);

            if (request.User.Role == Roles.Employer && request.User.EmployerId != null) 
                query = query.Where(x => x.EmployerId == request.User.EmployerId);

            var response = new SyncMissionResponse();

            response.Missions = await query
                .ProjectTo<SyncMissionQueryDto>(_mapper.ConfigurationProvider)
                .ToSyncResponseAsync<SyncMissionQueryDto, SyncMissionDto>(request.InitialSync, _mapper);

            if (response.Missions == null || response.Missions.Entities.Count() == 0)
                return response;

            var missionIds = response.Missions.Entities.Select(x => x.Id).ToHashSet();

            if (request.User.Role != Roles.Employer)
            {
                response.MissionDocuments = await _dbContext.MissionDocuments
                   .GetSyncItems(request, true)
                   .Where(x => missionIds.Contains(x.MissionId))
                   .ProjectTo<SyncMissionDocumentQueryDto>(_mapper.ConfigurationProvider)
                   .ToSyncResponseAsync<SyncMissionDocumentQueryDto, SyncMissionDocumentDto>(request.InitialSync, _mapper);

                response.MissionNotes = await _dbContext.MissionNotes
                   .GetSyncItems(request, true)
                   .Where(x => missionIds.Contains(x.MissionId))
                   .ProjectTo<SyncMissionNoteQueryDto>(_mapper.ConfigurationProvider)
                   .ToSyncResponseAsync<SyncMissionNoteQueryDto, SyncMissionNoteDto>(request.InitialSync, _mapper);
            }

            response.MissionImages = await _dbContext.MissionImages
                .GetSyncItems(request, true)
                .Where(x => missionIds.Contains(x.MissionId))
                .ProjectTo<SyncMissionImageQueryDto>(_mapper.ConfigurationProvider)
                .ToSyncResponseAsync<SyncMissionImageQueryDto, SyncMissionImageDto>(request.InitialSync, _mapper);

            response.MissionActivities = await _dbContext.MissionActivities
                .GetSyncItems(request, true)
                .Where(x => missionIds.Contains(x.MissionId))
                .ProjectTo<SyncMissionActivityQueryDto>(_mapper.ConfigurationProvider)
                .ToSyncResponseAsync<SyncMissionActivityQueryDto, SyncMissionActivityDto>(request.InitialSync, _mapper);

            return response;
        }

    }
}
