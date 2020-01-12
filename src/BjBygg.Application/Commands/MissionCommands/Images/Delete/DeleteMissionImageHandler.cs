using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Images.Delete
{
    public class DeleteMissionImageHandler : IRequestHandler<DeleteMissionImageCommand, bool>
    {
        private readonly AppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;

        public DeleteMissionImageHandler(AppDbContext dbContext, IBlobStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }

        public async Task<bool> Handle(DeleteMissionImageCommand request, CancellationToken cancellationToken)
        {
            var missionImage =  await _dbContext.Set<MissionImage>().FindAsync(request.Id);
            if (missionImage == null) return false;

            _dbContext.Set<MissionImage>().Remove(missionImage);
            await _dbContext.SaveChangesAsync();

            await _storageService.DeleteAsync(missionImage.FileURL.ToString());

            return true;
        }
    }
}
