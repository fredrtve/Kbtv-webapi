using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

            //Initiate calendar & week rules. 
            CultureInfo cultureInfo = new CultureInfo("nb-NO", false);
            Calendar calendar = cultureInfo.Calendar;
            CalendarWeekRule weekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            //Get startTime year and weekNr
            int year = timesheet.StartTime.Year;
            int weekNr = calendar.GetWeekOfYear(timesheet.StartTime, weekRule, firstDayOfWeek);

            //Check for existing timesheet week for given year and weekNr
            var existingWeek = (await _dbContext.Set<TimesheetWeek>()
                .Where(x => x.Year == year && x.WeekNr == weekNr)
                .ToListAsync()).FirstOrDefault();

           
            if (existingWeek != null && existingWeek.Status == TimesheetStatus.Open)  //Add to existing week if it exists & status is open. 
                timesheet.TimesheetWeekId = existingWeek.Id;         
            else if (existingWeek != null) //If week exist but is not open throw exception
                throw new BadRequestException($"Week {weekNr} is locked, new entries are forbidden.");
            else  //If week doesnt exist create it
            {
                existingWeek = new TimesheetWeek() { Year = year, WeekNr = weekNr, Status = TimesheetStatus.Open, UserName = request.UserName };
                _dbContext.Set<TimesheetWeek>().Add(existingWeek);
                await _dbContext.SaveChangesAsync();
                timesheet.TimesheetWeekId = existingWeek.Id;
            }
                
            _dbContext.Set<Timesheet>().Add(timesheet);        
            await _dbContext.SaveChangesAsync();

            timesheet.TimesheetWeek = existingWeek;

            return _mapper.Map<TimesheetDto>(timesheet);
        }
    }
}
