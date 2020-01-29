using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionReportTypeCommands.Update
{
    public class UpdateMissionReportTypeHandler : IRequestHandler<UpdateMissionReportTypeCommand, MissionReportTypeDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMissionReportTypeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionReportTypeDto> Handle(UpdateMissionReportTypeCommand request, CancellationToken cancellationToken)
        {
            var missionReportType = _mapper.Map<MissionReportType>(request);
            
            try
            {
                _dbContext.Entry(missionReportType).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");
            }

            return _mapper.Map<MissionReportTypeDto>(missionReportType);
        }
    }
}
