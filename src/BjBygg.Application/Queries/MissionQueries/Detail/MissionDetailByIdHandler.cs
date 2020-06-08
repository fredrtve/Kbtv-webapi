using CleanArchitecture.Infrastructure.Data;
using MediatR;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;

namespace BjBygg.Application.Queries.MissionQueries.Detail
{
    public class MissionDetailByIdHandler : IRequestHandler<MissionDetailByIdQuery, MissionDetailByIdResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionDetailByIdHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<MissionDetailByIdResponse> Handle(MissionDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var mission = (await _dbContext.Set<Mission>().AsQueryable()
                .Where(m => m.Id == request.Id)
                .Include(m => m.MissionImages)
                .Include(m => m.MissionDocuments)
                .ThenInclude(d => d.DocumentType)
                .Include(m => m.MissionType)
                .Include(m => m.MissionNotes)
                .Include(m => m.Employer)
                .ToListAsync()).FirstOrDefault();

            if (mission == null)
                throw new EntityNotFoundException($"Mission does not exist with id {request.Id}");

            mission.MissionNotes = mission.MissionNotes.OrderByDescending(a => a.CreatedAt).ToList();
            mission.MissionImages = mission.MissionImages.OrderByDescending(a => a.CreatedAt).ToList();

            return _mapper.Map<MissionDetailByIdResponse>(mission);
        }
    }
}
