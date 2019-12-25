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
        private readonly IMissionImageUploader _imageUploader;

        public DeleteMissionImageHandler(IMissionImageUploader imageUploader)
        {
            _imageUploader = imageUploader;
        }

        public async Task<bool> Handle(DeleteMissionImageCommand request, CancellationToken cancellationToken)
        {
            return await _imageUploader.DeleteImage(request.Id);
        }
    }
}
