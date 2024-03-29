using AutoMapper;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using BjBygg.Core.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.CreateTimesheet
{
    public class CreateTimesheetCommandHandler : IRequestHandler<CreateTimesheetCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateTimesheetCommandHandler(IAppDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(CreateTimesheetCommand request, CancellationToken cancellationToken)
        {
            var timesheet = _mapper.Map<Timesheet>(request);

            timesheet.TotalHours = Math.Round((timesheet.EndTime - timesheet.StartTime).TotalHours, 1);

            if(_currentUserService.Role != Roles.Leader || request.UserName == null)          
                timesheet.UserName = _currentUserService.UserName;
            
            timesheet.Status = TimesheetStatus.Open;

            _dbContext.Set<Timesheet>().Add(timesheet);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
