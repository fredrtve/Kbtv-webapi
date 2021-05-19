using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Entities;
using BjBygg.Core.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.DeleteTimesheet
{
    public class DeleteTimesheetHandler : IRequestHandler<DeleteTimesheetCommand>
    {
        private readonly IAppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTimesheetHandler(IAppDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteTimesheetCommand request, CancellationToken cancellationToken)
        {
            var timesheet = await _dbContext.Set<Timesheet>().FindAsync(request.Id);

            if (timesheet == null) return Unit.Value;

            if (_currentUserService.Role != Roles.Leader) //Allow leader
            {
                if (_currentUserService.UserName != timesheet.UserName)
                    throw new ForbiddenException();

                if (timesheet.Status != TimesheetStatus.Open)
                    throw new BadRequestException("Timesheet is closed for manipulation.");
            }

            _dbContext.Set<Timesheet>().Remove(timesheet);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
