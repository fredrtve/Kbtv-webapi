using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll;
using BjBygg.Application.Common;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Queries.DbSyncQueries
{
    public class MissionSyncQuery : UserDbSyncQuery, IRequest<MissionSyncArraysResponse> { }

    public class MissionSyncQueryHandler : IRequestHandler<MissionSyncQuery, MissionSyncArraysResponse>
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

        public async Task<MissionSyncArraysResponse> Handle(MissionSyncQuery request, CancellationToken cancellationToken)
        {
            var latestUpdate = _syncTimestamps.Timestamps[typeof(Mission)];
            if (!request.InitialSync && latestUpdate != null && latestUpdate <= request.Timestamp) return null;

            var query = _dbContext.Set<Mission>().AsQueryable();
            var isEmployer = request.User.Role == Roles.Employer;
            var isInitial = request.InitialSync;

            if (isEmployer && request.User.EmployerId != null) //Only allow employers missions if role is employer & employerId is set
                query = query.Where(x => x.EmployerId == request.User.EmployerId);

            query = query.GetSyncItems(request).Include(x => x.MissionImages);

            if (!isEmployer)
                query = query.Include(x => x.MissionDocuments).Include(x => x.MissionNotes);

            var missions = await query.ToListAsync();

            return new MissionSyncArraysResponse 
            {
                Missions = missions.ToSyncArrayResponse<Mission, MissionDto>(isInitial, _mapper),

                MissionImages =
                    missions.SelectMany(x => x.MissionImages).GetChildSyncItems(request)
                        .ToSyncArrayResponse<MissionImage, MissionImageDto>(isInitial, _mapper),

                MissionDocuments = isEmployer ? null :
                    missions.SelectMany(x => x.MissionDocuments).GetChildSyncItems(request)
                        .ToSyncArrayResponse<MissionDocument, MissionDocumentDto>(isInitial, _mapper),

                MissionNotes = isEmployer ? null :
                    missions.SelectMany(x => x.MissionNotes).GetChildSyncItems(request)
                        .ToSyncArrayResponse<MissionNote, MissionNoteDto>(isInitial, _mapper)
        };
        }

    }
}
