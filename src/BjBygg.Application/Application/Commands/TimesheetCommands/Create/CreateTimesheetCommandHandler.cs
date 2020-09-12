using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Create
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

            timesheet.StartTime = DateTimeHelper.ConvertEpochToDate(request.StartTime); 
            timesheet.EndTime = DateTimeHelper.ConvertEpochToDate(request.EndTime);

            timesheet.TotalHours = (timesheet.EndTime - timesheet.StartTime).TotalHours;

            timesheet.UserName = _currentUserService.UserName;

            _dbContext.Set<Timesheet>().Add(timesheet);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
