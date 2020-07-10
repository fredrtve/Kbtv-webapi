using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Create
{
    public class CreateMissionCommandHandler : IRequestHandler<CreateMissionCommand, MissionDto>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _storageService;

        public CreateMissionCommandHandler(IAppDbContext dbContext, IMapper mapper, IBlobStorageService storageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<MissionDto> Handle(CreateMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = _mapper.Map<Mission>(request);
            var employer = mission.Employer;
            var type = mission.MissionType;

            if (mission.Employer != null)
            {
                //If no changes are made
                if (mission.Employer.Id > 0 || String.IsNullOrEmpty(mission.Employer.Name))
                    mission.Employer = null;
            }

            if (mission.MissionType != null)
            {
                //If no changes are made
                if (mission.MissionType.Id > 0 || String.IsNullOrEmpty(mission.MissionType.Name))
                    mission.MissionType = null;
            }

            if (request.Image != null)
            {
                mission.ImageURL = await _storageService.UploadFileAsync(request.Image, ResourceFolderConstants.Image);
            }

            await _dbContext.Set<Mission>().AddAsync(mission);

            await _dbContext.SaveChangesAsync();

            //Assign values after creation to prevent unwanted changes
            mission.Employer = employer;
            mission.MissionType = type;

            return _mapper.Map<MissionDto>(mission);
        }
    }
}
