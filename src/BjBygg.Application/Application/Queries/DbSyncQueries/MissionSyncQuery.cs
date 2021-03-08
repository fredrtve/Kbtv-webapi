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
    public class MissionSyncQuery : UserDbSyncQuery, IRequest<MissionSyncArraysResponse> {}

    public class MissionSyncQueryHandler : IRequestHandler<MissionSyncQuery, MissionSyncArraysResponse>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionSyncQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<MissionSyncArraysResponse> Handle(MissionSyncQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Mission>().AsQueryable();

            if (request.User.Role == Roles.Employer) //Only allow employers missions if role is employer
                query = query.Where(x => x.EmployerId == request.User.EmployerId); 

            var missions = await query
                .GetSyncItems(request)
                .Include(x => x.MissionDocuments)
                .Include(x => x.MissionNotes)
                .Include(x => x.MissionImages)
                .ToListAsync();

            var isInitial = request.Timestamp == null;

            return new MissionSyncArraysResponse()
            {
                MissionDocuments = missions.SelectMany(x => x.MissionDocuments).GetMissionChildSyncItems(request)
                    .ToSyncArrayResponse<MissionDocument, MissionDocumentDto>(isInitial, _mapper),
                MissionImages = missions.SelectMany(x => x.MissionImages).GetMissionChildSyncItems(request)
                    .ToSyncArrayResponse<MissionImage, MissionImageDto>(isInitial, _mapper),
                MissionNotes = missions.SelectMany(x => x.MissionNotes).GetMissionChildSyncItems(request)
                    .ToSyncArrayResponse<MissionNote, MissionNoteDto>(isInitial, _mapper),
                Missions = missions.ToSyncArrayResponse<Mission, MissionDto>(isInitial, _mapper)
            };
        }

    }
}
