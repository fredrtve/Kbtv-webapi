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

namespace BjBygg.Application.Application.Commands.MissionCommands.UpdateHeaderImage
{
    public class UpdateMissionHeaderImageCommandHandler : IRequestHandler<UpdateMissionHeaderImageCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _storageService;

        public UpdateMissionHeaderImageCommandHandler(IAppDbContext dbContext, IMapper mapper, IBlobStorageService storageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<Unit> Handle(UpdateMissionHeaderImageCommand request, CancellationToken cancellationToken)
        {
            var dbMission = await _dbContext.Missions.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbMission == null)
                throw new EntityNotFoundException(nameof(Mission), request.Id);

            dbMission.FileUri = await _storageService.UploadFileAsync(request.Image, ResourceFolderConstants.MissionHeader);
            //if (request.Image != null)
            //{
            //    var resized = _imageResizer.ResizeImage(request.Image, 700);
            //    resized.FileName = $"{mission.Id}.{resized.FileExtension}";
            //    var res = await _storageService.UploadFileAsync(resized, ResourceFolderConstants.MissionHeader);
            //    if (res != null) mission.FileUri = new Uri(resized.FileName);
            //}
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}