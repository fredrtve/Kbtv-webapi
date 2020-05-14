using AutoMapper;
using BjBygg.Application.Queries.MissionQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace BjBygg.Application.Commands.TimesheetCommands.Update
{
    public class UpdateTimesheetHandler : IRequestHandler<UpdateTimesheetCommand, TimesheetDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateTimesheetHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TimesheetDto> Handle(UpdateTimesheetCommand request, CancellationToken cancellationToken)
        {
            var dbTimesheet = await _dbContext.Timesheets.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbTimesheet == null)
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");

            if (dbTimesheet.UserName != request.UserName) //Cant change timesheet user identity
                throw new UnauthorizedException($"Timesheet does not belong to user {request.UserName}");

            if (dbTimesheet.Status != TimesheetStatus.Open) //Cant change timesheet user identity
                throw new UnauthorizedException($"Timesheet required to be open");

            foreach (var property in request.GetType().GetProperties())
            {
                if (property.Name == "Id" || property.Name == "UserName") continue;
                dbTimesheet.GetType().GetProperty(property.Name).SetValue(dbTimesheet, property.GetValue(request), null);
            }

            TimeZoneInfo timeInfo = TZConvert.GetTimeZoneInfo("Central Europe Standard Time");

            dbTimesheet.StartTime = TimeZoneInfo.ConvertTime(dbTimesheet.StartTime, timeInfo);
            dbTimesheet.EndTime = TimeZoneInfo.ConvertTime(dbTimesheet.EndTime, timeInfo);

            dbTimesheet.TotalHours = (dbTimesheet.EndTime - dbTimesheet.StartTime).TotalHours;

            try { await _dbContext.SaveChangesAsync(); }
            catch (Exception ex)
            {
                throw new BadRequestException($"Something went wrong when storing your request.");
            }

  

            //Assign values after creation to prevent unwanted changes
            return _mapper.Map<TimesheetDto>(dbTimesheet);
        }
    }
}
