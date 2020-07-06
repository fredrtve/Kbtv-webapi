using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeZoneConverter;

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
            //Timezone is converted back as they get converted by api. Should be globally configured.
            TimeZoneInfo timeInfo = TZConvert.GetTimeZoneInfo("Central Europe Standard Time");

            timesheet.StartTime = TimeZoneInfo.ConvertTime(timesheet.StartTime, timeInfo);
            timesheet.EndTime = TimeZoneInfo.ConvertTime(timesheet.EndTime, timeInfo);

            timesheet.TotalHours = (timesheet.EndTime - timesheet.StartTime).TotalHours;
   
            _dbContext.Set<Timesheet>().Add(timesheet);        
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TimesheetDto>(timesheet);
        }
    }
}
