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
    public class UpdateMissionCommandHandler : IRequestHandler<UpdateMissionCommand, MissionDto>
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

        public async Task<MissionDto> Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
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
                        if (request.MissionType == null) continue;
                        if ((request.MissionType.Id ?? 0) == 0 && !String.IsNullOrWhiteSpace(request.MissionType.Name)) //If id is not present but name is, create new entity
                            dbMission.MissionType = new MissionType() { Name = request.MissionType.Name }; 
                        else 
                            dbMission.MissionTypeId = request.MissionType.Id;
                        break;
                    case "Employer":
                        if (request.Employer == null) continue;
                        if ((request.Employer.Id ?? 0) == 0 && !String.IsNullOrWhiteSpace(request.Employer.Name)) 
                            dbMission.Employer = new Employer() { Name = request.Employer.Name };                       
                        else 
                            dbMission.EmployerId = request.Employer.Id;                  
                        break;
                    case "DeleteCurrentImage":
                        if (request.DeleteCurrentImage == true)
                            dbMission.ImageURL = null;
                        break;
                    case "Image":
                        if (request.Image != null)
                            dbMission.ImageURL = await _storageService.UploadFileAsync(request.Image, ResourceFolderConstants.Image);
                        break;
                    default:
                        dbMission.GetType().GetProperty(property.Name).SetValue(dbMission, property.GetValue(request), null);
                        break;
                }
            }

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<MissionDto>(dbMission);
        }
    }
}
