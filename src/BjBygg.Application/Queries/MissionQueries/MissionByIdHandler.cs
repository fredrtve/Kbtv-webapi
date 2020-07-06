using CleanArchitecture.Infrastructure.Data;
using MediatR;
using CleanArchitecture.Core.Entities;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BjBygg.Application.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CleanArchitecture.Core.Exceptions;

namespace BjBygg.Application.Queries.MissionQueries
{
    public class MissionByIdHandler : IRequestHandler<MissionByIdQuery, MissionDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionByIdHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<MissionDto> Handle(MissionByIdQuery request, CancellationToken cancellationToken)
        {
            var mission = await _dbContext.Set<Mission>()
                .Where(x => x.Id == request.Id)
                .Include(x => x.Employer)
                .Include(x => x.MissionType)
                .ToListAsync();

            if (mission == null) throw new EntityNotFoundException($"Mission does not exist with id {request.Id}");

            return _mapper.Map<MissionDto>(mission.FirstOrDefault());
        }
    }
}
