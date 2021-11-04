using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.UpdateTimesheet
{
    public class UpdateTimesheetCommandHandler : IRequestHandler<UpdateTimesheetCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTimesheetCommandHandler(IAppDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateTimesheetCommand request, CancellationToken cancellationToken)
        {
            var dbTimesheet = await _dbContext.Timesheets.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbTimesheet == null)
                throw new EntityNotFoundException(nameof(Timesheet), request.Id);

            if (_currentUserService.Role != Roles.Leader && dbTimesheet.UserName != _currentUserService.UserName)
                throw new ForbiddenException();

            if (dbTimesheet.Status != TimesheetStatus.Open)
                throw new BadRequestException($"Timesheet is closed for manipulation.");

            dbTimesheet.Comment = request.Comment;
            dbTimesheet.MissionActivityId = request.MissionActivityId;
            dbTimesheet.StartTime = DateTimeHelper.ConvertEpochToDate(request.StartTime / 1000);
            dbTimesheet.EndTime = DateTimeHelper.ConvertEpochToDate(request.EndTime / 1000);

            dbTimesheet.TotalHours = (dbTimesheet.EndTime - dbTimesheet.StartTime).TotalHours;

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
