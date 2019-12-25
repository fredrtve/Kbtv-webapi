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

namespace BjBygg.Application.Queries.MissionQueries.Detail
{
    public class MissionDetailByIdHandler : IRequestHandler<MissionDetailByIdQuery, MissionDetailByIdResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionDetailByIdHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionDetailByIdResponse> Handle(MissionDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var mission = (await _dbContext.Set<Mission>().AsQueryable()
                .Where(m => m.Id == request.Id)
                .Include(m => m.MissionImages)
                .Include(m => m.MissionType)
                .Include(m => m.MissionNotes)
                .Include(m => m.Employer)
                .ToListAsync()).FirstOrDefault();
            mission.MissionNotes = mission.MissionNotes.OrderByDescending(a => a.CreatedAt).ToList();
            mission.MissionImages = mission.MissionImages.OrderByDescending(a => a.CreatedAt).ToList();
            return _mapper.Map<MissionDetailByIdResponse>(mission);
        }
    }
}
