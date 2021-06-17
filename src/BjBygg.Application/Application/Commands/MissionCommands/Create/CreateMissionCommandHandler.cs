using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using System;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Create
{
    public class CreateMissionCommandHandler : CreateCommandHandler<Mission, CreateMissionCommand>
    {
        private readonly IGeocodeService _geocoderService;

        public CreateMissionCommandHandler(IAppDbContext dbContext, IMapper mapper, IGeocodeService geocoderService) :
            base(dbContext, mapper)
        {
            _geocoderService = geocoderService;
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
            
        }
    }
}
