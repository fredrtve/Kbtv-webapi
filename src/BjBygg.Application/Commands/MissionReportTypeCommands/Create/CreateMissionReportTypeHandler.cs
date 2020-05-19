using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.ReportTypeCommands.Create
{
    public class CreateMissionTypeHandler : IRequestHandler<CreateReportTypeCommand, ReportTypeDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMissionTypeHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ReportTypeDto> Handle(CreateReportTypeCommand request, CancellationToken cancellationToken)
        {
            var ReportType = new ReportType() { Name = request.Name };
            _dbContext.Set<ReportType>().Add(ReportType);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ReportTypeDto>(ReportType);
        }
    }
}
