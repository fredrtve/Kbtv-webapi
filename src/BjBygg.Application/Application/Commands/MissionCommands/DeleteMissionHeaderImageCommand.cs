using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands
{
    public class DeleteMissionHeaderImageCommand : IRequest
    {
        public string Id { get; set; }
    }

    public class DeleteMissionHeaderImageCommandValidator : AbstractValidator<DeleteMissionHeaderImageCommand>
    {
        public DeleteMissionHeaderImageCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().WithName("Id");
        }
    }

    public class DeleteMissionHeaderImageCommandHandler : IRequestHandler<DeleteMissionHeaderImageCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IBlobStorageService _blobService;
        private readonly ResourceFolders _folders;

        public DeleteMissionHeaderImageCommandHandler(IAppDbContext dbContext, IBlobStorageService blobService, IOptions<ResourceFolders> folders)
        {
            _dbContext = dbContext;
            _blobService = blobService;
            _folders = folders.Value;
        }

        public async Task<Unit> Handle(DeleteMissionHeaderImageCommand request, CancellationToken cancellationToken)
        {
            var mission = await _dbContext.Set<Mission>().FindAsync(request.Id);

            if (mission == null)
                throw new EntityNotFoundException(nameof(Mission), request.Id);

            if (mission.FileName == null) return Unit.Value;

            await _blobService.DeleteAsync(mission.FileName, _folders.MissionHeader);

            mission.FileName = null;

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
