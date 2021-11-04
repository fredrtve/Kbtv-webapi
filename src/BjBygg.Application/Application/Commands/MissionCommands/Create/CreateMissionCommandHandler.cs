using AutoMapper;
using BjBygg.Application.Application.Common;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Create
{
    public class CreateMissionCommandHandler : CreateCommandHandler<Mission, CreateMissionCommand>
    {
        private readonly IGeocodeService _geocoderService;
        private readonly IIdGenerator _idGenerator;

        public CreateMissionCommandHandler(
            IAppDbContext dbContext, 
            IMapper mapper, 
            IGeocodeService geocoderService,
            IIdGenerator idGenerator) :
            base(dbContext, mapper)
        {
            _geocoderService = geocoderService;
            _idGenerator = idGenerator;
        }

        protected override async Task OnBeforeSavingAsync(CreateMissionCommand request, Mission entity) 
        {
            try
            {
                var position = await _geocoderService.GetPositionAsync(entity.Address);
                entity.Position = position;
            }catch(Exception ex)
            {
                entity.Position = null;
            }

            if (entity.MissionActivities.Count == 0)
            {
                var missionActivity = new MissionActivity() { Id = _idGenerator.Generate(), ActivityId = "default", MissionId = entity.Id };
                _dbContext.Set<MissionActivity>().Add(missionActivity);
            }

            var addressCount = await _dbContext.Set<Mission>().AddressCountAsync(request.Address);
            if (addressCount != 0) entity.Address = request.Address + " (" + ++addressCount + ")";
        }
    }
}
