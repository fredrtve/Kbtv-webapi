using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.BaseEntityCommands.Create;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
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
            var normalizedAddr = request.Address.ToUpper();
            var existingAddresses = (await _dbContext.Set<Mission>().ToListAsync())
                .Where(x => x.Address.ToUpper().Contains(normalizedAddr)).Count();
            if (existingAddresses != 0) entity.Address = entity.Address + " (" + ++existingAddresses + ")";
        }
    }
}
