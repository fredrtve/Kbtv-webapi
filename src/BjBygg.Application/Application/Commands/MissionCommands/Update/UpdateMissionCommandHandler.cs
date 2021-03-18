using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionCommandHandler : IRequestHandler<UpdateMissionCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMissionCommandHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
        {
            var dbEntity = await _dbContext.Set<Mission>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbEntity == null)
                throw new EntityNotFoundException(nameof(Mission), request.Id);

            var ignoredProps = new List<string>() { "Id", "MissionType", "Employer" };
            foreach (var property in request.GetType().GetProperties())
            {
                if (ignoredProps.Contains(property.Name)) continue;
                dbEntity.GetType().GetProperty(property.Name).SetValue(dbEntity, property.GetValue(request), null);
            }

            dbEntity.Employer =
                request.Employer != null ? _mapper.Map<Employer>(request.Employer) : null;

            dbEntity.MissionType =
                request.MissionType != null ? _mapper.Map<MissionType>(request.MissionType) : null;

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
