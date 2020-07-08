using AutoMapper;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Enums;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Interfaces.Services;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeZoneConverter;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Commands.TimesheetCommands.Update
{
    public class UpdateTimesheetCommandHandler : IRequestHandler<UpdateTimesheetCommand, TimesheetDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTimesheetCommandHandler(AppDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TimesheetDto> Handle(UpdateTimesheetCommand request, CancellationToken cancellationToken)
        {
            var dbTimesheet = await _dbContext.Timesheets.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbTimesheet == null)
                throw new EntityNotFoundException(nameof(Timesheet), request.Id);

            if (dbTimesheet.UserName != _currentUserService.UserName) //Can only update self
                throw new ForbiddenException();

            if (dbTimesheet.Status != TimesheetStatus.Open)
                throw new BadRequestException($"Timesheet is closed for manipulation.");

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
