using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using CleanArchitecture.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Create
{
    public class CreateMissionCommandHandler : IRequestHandler<CreateMissionCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _storageService;
        private readonly IImageResizer _imageResizer;

        public CreateMissionCommandHandler(
            IAppDbContext dbContext,
            IMapper mapper,
            IBlobStorageService storageService,
            IImageResizer imageResizer)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _storageService = storageService;
            _imageResizer = imageResizer;
        }

        public async Task<Unit> Handle(CreateMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = _mapper.Map<Mission>(request);

            await _dbContext.Set<Mission>().AddAsync(mission);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
