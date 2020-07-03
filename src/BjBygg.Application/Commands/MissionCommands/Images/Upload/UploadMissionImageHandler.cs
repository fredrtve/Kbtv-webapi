using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageHandler : IRequestHandler<UploadMissionImageCommand, IEnumerable<MissionImageDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IBlobStorageService _storageService;
        private readonly IMapper _mapper;

        public UploadMissionImageHandler(AppDbContext dbContext, IBlobStorageService storageService, IMapper mapper)
        {
            _dbContext = dbContext;
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MissionImageDto>> Handle(UploadMissionImageCommand request, CancellationToken cancellationToken)
        {
            List<MissionImage> images = new List<MissionImage>();

            if(request.Files != null && request.Files.Count != 0)
            {           
                var imageUrls = await _storageService.UploadFilesAsync(request.Files, ResourceFolderConstants.Image);

                images.AddRange(
                    imageUrls.Select(url => new MissionImage() { MissionId = request.MissionId, FileURL = url }));

                try
                {
                    _dbContext.Set<MissionImage>().AddRange(images);
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new EntityNotFoundException($"Invalid foreign key");
                }         
            }

            return images.Select(x => _mapper.Map<MissionImageDto>(x));
        }
    }
}
