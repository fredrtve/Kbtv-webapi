using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommandHandler : IRequestHandler<UpdateMissionCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _storageService;

        public UpdateMissionCommandHandler(IAppDbContext dbContext, IMapper mapper, IBlobStorageService storageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<Unit> Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
        {
            var dbMission = await _dbContext.Missions
                .Include(x => x.MissionType)
                .Include(x => x.Employer)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbMission == null)
                throw new EntityNotFoundException(nameof(Mission), request.Id);

            //var mission = _mapper.Map<Mission>(request);
            foreach (var property in request.GetType().GetProperties())
            {
                switch (property.Name)
                {
                    case "Id": break;
                    case "MissionType":
                        dbMission = await CheckMissionTypeProperty(dbMission);
                        break;
                    case "Employer":
                        dbMission = await CheckEmployerProperty(dbMission);
                        break;
                    case "DeleteCurrentImage":
                        if (request.DeleteCurrentImage == true)
                            dbMission.FileUri = null;
                        break;
                    default:
                        dbMission.GetType().GetProperty(property.Name).SetValue(dbMission, property.GetValue(request), null);
                        break;
                }
            }

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task<Mission> CheckMissionTypeProperty(Mission mission)
        {
            if (mission == null || mission.MissionType == null)
                return mission;

            if (String.IsNullOrWhiteSpace(mission.MissionType.Id))
            {
                mission.MissionType = null;
                return mission;
            }

            var type = mission.MissionType;
            
            var dbMissionType = await _dbContext.MissionTypes.FindAsync(type.Id);
            if (dbMissionType != null)
            {
                    mission.MissionTypeId = type.Id;
                    mission.MissionType = null;
            }
            else if (String.IsNullOrWhiteSpace(type.Name)) mission.MissionType = null;

            return mission;       
        }
        private async Task<Mission> CheckEmployerProperty(Mission mission)
        {
            if (mission == null || mission.Employer == null)
                return mission;

            if (String.IsNullOrWhiteSpace(mission.MissionType.Id))
            {
                mission.Employer = null;
                return mission;
            }

            var employer = mission.Employer;

            var dbEmployer = await _dbContext.Employers.FindAsync(employer.Id);
            if (dbEmployer != null)
            {
                mission.EmployerId = employer.Id;
                mission.Employer = null;
            }
            else if (String.IsNullOrWhiteSpace(employer.Name)) mission.Employer = null;

            return mission;
        }
    }
}
