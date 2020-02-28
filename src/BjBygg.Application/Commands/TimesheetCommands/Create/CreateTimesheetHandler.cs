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

namespace BjBygg.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetHandler : IRequestHandler<CreateTimesheetCommand, TimesheetDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateTimesheetHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TimesheetDto> Handle(CreateTimesheetCommand request, CancellationToken cancellationToken)
        {
            var timesheet = _mapper.Map<Timesheet>(request);

            _dbContext.Set<Timesheet>().Add(timesheet);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TimesheetDto>(timesheet);
        }
    }
}
