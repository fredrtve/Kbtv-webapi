using CleanArchitecture.Infrastructure.Data;
using MediatR;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace BjBygg.Application.Queries.MissionQueries
{
    public class MissionByIdHandler : IRequestHandler<MissionByIdQuery, MissionByIdResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MissionByIdHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionByIdResponse> Handle(MissionByIdQuery request, CancellationToken cancellationToken)
        {
            var mission = await _dbContext.Set<Mission>().FindAsync(request.Id);
            return _mapper.Map<MissionByIdResponse>(mission);
        }
    }
}
