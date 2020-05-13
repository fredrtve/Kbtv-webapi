using AutoMapper;
using BjBygg.Application.Queries.MissionQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionHandler : IRequestHandler<UpdateMissionCommand, MissionDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMissionHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionDto> Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
        {
            var dbMission = await _dbContext.Missions
                .Include(x => x.MissionType)
                .Include(x => x.Employer)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbMission == null)
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            var mission = _mapper.Map<Mission>(request);
            foreach (var property in mission.GetType().GetProperties())
            {
                if (property.Name == "Id") continue;
                if (property.Name == "MissionType" && mission.MissionType != null && mission.MissionType.Id != 0)
                    dbMission.MissionTypeId = mission.MissionType.Id; //Update only ID if existing entity
                else if (property.Name == "Employer" && mission.Employer != null && mission.Employer.Id != 0)
                    dbMission.EmployerId = mission.Employer.Id; //Update only ID if existing entity
                else property.SetValue(dbMission, property.GetValue(mission), null);
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex){
                throw new BadRequestException($"Something went wrong when trying to store your request.");
            }

            return _mapper.Map<MissionDto>(dbMission);
        }
    }
}
