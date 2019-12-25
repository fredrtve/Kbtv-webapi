using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Create
{
    public class CreateMissionHandler : IRequestHandler<CreateMissionCommand, int>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMissionHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = _mapper.Map<Mission>(request);

            _dbContext.Set<Mission>()
                .Add(mission);

            await _dbContext.SaveChangesAsync();

            return mission.Id;
        }
    }
}
