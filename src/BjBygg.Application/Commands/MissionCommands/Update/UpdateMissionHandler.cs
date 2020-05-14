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

            //var mission = _mapper.Map<Mission>(request);
            foreach (var property in request.GetType().GetProperties())
            {
                if (property.Name == "Id") continue; //Skip id
                if (property.Name == "MissionType") {
                    if (request.MissionType == null) continue;
                    if ((request.MissionType.Id ?? 0) != 0) //If id is not 0 or null
                        dbMission.MissionTypeId = request.MissionType.Id;
                    else if (!String.IsNullOrWhiteSpace(request.MissionType.Name)) //If name is present but no id, create
                        dbMission.MissionType = new MissionType() { Name = request.MissionType.Name };
                    else dbMission.MissionType = null; //No new or existing added
                    continue;
                }
                else if (property.Name == "Employer")
                {
                    if (request.Employer == null) continue;
                    if ((request.Employer.Id ?? 0) != 0) //If id is not 0 or null
                        dbMission.EmployerId = request.Employer.Id;
                    else if (!String.IsNullOrWhiteSpace(request.Employer.Name)) //If name is present but no id, create
                        dbMission.Employer = new Employer() { Name = request.Employer.Name };
                    else dbMission.Employer = null; //No new or existing added
                    continue;
                }
                else
                    dbMission.GetType().GetProperty(property.Name).SetValue(dbMission, property.GetValue(request), null);
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
