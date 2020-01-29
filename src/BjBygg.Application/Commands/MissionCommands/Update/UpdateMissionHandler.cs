using AutoMapper;
using BjBygg.Application.Queries.MissionQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionCommands.Update
{
    public class UpdateMissionHandler : IRequestHandler<UpdateMissionCommand, MissionDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMissionHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MissionDto> Handle(UpdateMissionCommand request, CancellationToken cancellationToken)
        {
            var mission = _mapper.Map<Mission>(request);

            if (mission.MissionType != null)
            {
                if (mission.MissionType.Id == 0 && !String.IsNullOrEmpty(mission.MissionType.Name)) //Add new type if only name is present
                  _dbContext.Set<MissionType>().Add(mission.MissionType);

                else if (String.IsNullOrEmpty(mission.MissionType.Name)) //Prevent wiping name of existing type if no name is present
                  mission.MissionType = null;
            }

            if (mission.Employer != null)
            {
                if (mission.Employer.Id == 0 && !String.IsNullOrEmpty(mission.Employer.Name))
                  _dbContext.Set<Employer>().Add(mission.Employer);
 
                else if (String.IsNullOrEmpty(mission.Employer.Name))
                  mission.Employer = null;
            }

            try
            {
                _dbContext.Entry(mission).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex){
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");
            }
                   
            return _mapper.Map<MissionDto>(mission);
        }
    }
}
