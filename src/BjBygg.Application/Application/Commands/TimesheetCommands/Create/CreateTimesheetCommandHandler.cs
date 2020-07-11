using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetCommandHandler : IRequestHandler<CreateTimesheetCommand, TimesheetDto>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateTimesheetCommandHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TimesheetDto> Handle(CreateTimesheetCommand request, CancellationToken cancellationToken)
        {
            var timesheet = _mapper.Map<Timesheet>(request);

            timesheet.StartTime = DateTimeHelper.ConvertEpochToDate(request.StartTime); 
            timesheet.EndTime = DateTimeHelper.ConvertEpochToDate(request.EndTime);

            timesheet.TotalHours = (timesheet.EndTime - timesheet.StartTime).TotalHours;

            _dbContext.Set<Timesheet>().Add(timesheet);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TimesheetDto>(timesheet);
        }
    }
}
