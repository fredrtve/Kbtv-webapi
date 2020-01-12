using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
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
            var images = new List<MissionImage>();

            if(request.Files != null && request.Files.Count != 0)
            {
                var imageURIs = await _storageService.UploadAsync(request.Files);
                foreach (var uri in imageURIs)
                {
                    images.Add(new MissionImage() { MissionId = request.MissionId, FileURL = uri });
                }

                _dbContext.Set<MissionImage>().AddRange(images);
                await _dbContext.SaveChangesAsync();
            }

            return images.Select(x => _mapper.Map<MissionImageDto>(x));
        }
    }
}
