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

namespace BjBygg.Application.Commands.MissionReportTypeCommands.Create
{
    public class CreateMissionTypeHandler : IRequestHandler<CreateMissionReportTypeCommand, MissionReportTypeDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMissionTypeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionReportTypeDto> Handle(CreateMissionReportTypeCommand request, CancellationToken cancellationToken)
        {
            var missionReportType = new MissionReportType() { Name = request.Name };
            _dbContext.Set<MissionReportType>().Add(missionReportType);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<MissionReportTypeDto>(missionReportType);
        }
    }
}
