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

namespace BjBygg.Application.Commands.MissionTypeCommands.Update
{
    public class UpdateMissionTypeHandler : IRequestHandler<UpdateMissionTypeCommand, MissionTypeDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMissionTypeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionTypeDto> Handle(UpdateMissionTypeCommand request, CancellationToken cancellationToken)
        {
            var missionType = _mapper.Map<MissionType>(request);
     
            try
            {
                _dbContext.Entry(missionType).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");
            }

            return _mapper.Map<MissionTypeDto>(missionType);
        }
    }
}
