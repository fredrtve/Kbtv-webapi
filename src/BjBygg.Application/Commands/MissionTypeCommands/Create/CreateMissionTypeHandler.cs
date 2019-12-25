using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.MissionTypeCommands.Create
{
    public class CreateMissionTypeHandler : IRequestHandler<CreateMissionTypeCommand>
    {
        private readonly AppDbContext _dbContext;

        public CreateMissionTypeHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateMissionTypeCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Set<MissionType>()
                .Add(new MissionType() { Name = request.Name });

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
