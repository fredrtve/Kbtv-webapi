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

            //var employer = mission.Employer;
            //var type = mission.MissionType;

            //if (employer != null && !String.IsNullOrEmpty(employer.Id))
            //{
            //    var dbEmployer = await _dbContext.Employers.FindAsync(employer.Id);
            //    if (dbEmployer != null) {
            //        mission.EmployerId = employer.Id;
            //        mission.Employer = null;
            //    }
            //    else if (String.IsNullOrEmpty(employer.Name)) mission.Employer = null;
            //} 
            //else mission.Employer = null;

            //if (type != null && !String.IsNullOrEmpty(type.Id))
            //{
            //    var dbMissionType = await _dbContext.MissionTypes.FindAsync(type.Id);
            //    if (dbMissionType != null)
            //    {
            //        mission.MissionTypeId = type.Id;
            //        mission.MissionType = null;
            //    }
            //    else if (String.IsNullOrEmpty(type.Name)) mission.MissionType = null;
            //}
            //else mission.MissionType = null;

            await _dbContext.Set<Mission>().AddAsync(mission);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
