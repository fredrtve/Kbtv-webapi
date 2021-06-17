using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommandHandler : IRequestHandler<UpdateMissionCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IGeocodeService _geocoderService;

        public UpdateMissionCommandHandler(IAppDbContext dbContext, IMapper mapper, IGeocodeService geocoderService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _geocoderService = geocoderService;
        }

        public async Task<Unit> Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
        {
            var dbEntity = await _dbContext.Set<Mission>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbEntity == null)
                throw new EntityNotFoundException(nameof(Mission), request.Id);

            var ignoredProps = new HashSet<string>() { "Id", "MissionType", "Employer", "Address", "Position" };
            foreach (var property in request.GetType().GetProperties())
            {
                if (ignoredProps.Contains(property.Name)) continue;
                dbEntity.GetType().GetProperty(property.Name).SetValue(dbEntity, property.GetValue(request), null);
            }

            dbEntity.Employer =
                request.Employer != null ? _mapper.Map<Employer>(request.Employer) : null;

            dbEntity.MissionType =
                request.MissionType != null ? _mapper.Map<MissionType>(request.MissionType) : null;

            if(request.Address != null && request.Address != dbEntity.Address && request.Position == null)
            {
                dbEntity.Address = request.Address;
                try
                {
                    var position = await _geocoderService.GetPositionAsync(dbEntity.Address);
                    dbEntity.Position = position;
                }
                catch (Exception ex)
                {
                    dbEntity.Position = null;
                }
            }
            else if(request.Position != null)
            {
                dbEntity.Position = request.Position;
            }
         
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
