using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionTypeCommands.Create
{
    public class CreateMissionTypeHandler : IRequestHandler<CreateMissionTypeCommand, MissionTypeDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMissionTypeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionTypeDto> Handle(CreateMissionTypeCommand request, CancellationToken cancellationToken)
        {
            var missionType = new MissionType() { Name = request.Name };

            _dbContext.Set<MissionType>().Add(missionType);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<MissionTypeDto>(missionType);
        }
    }
}
