using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.TimesheetCommands
{
    public class ConfirmAllTimesheetsLastMonthCommand : IRequest {}

    public class ConfirmAllTimesheetsLastMonthHandler : IRequestHandler<ConfirmAllTimesheetsLastMonthCommand>
    {
        private readonly IAppDbContext _dbContext;

        public ConfirmAllTimesheetsLastMonthHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ConfirmAllTimesheetsLastMonthCommand request, CancellationToken cancellationToken)
        {
            var now = DateTimeHelper.NowLocalTime();

            var settings = await _dbContext.GetLeaderSettingsAsync();
            if (settings.ConfirmTimesheetsMonthly != true) return Unit.Value;

            var prevMonth = now.AddMonths(-1);
            var fromDate = new DateTimeOffset(prevMonth.Year, prevMonth.Month, 1, 0, 0, 0, new TimeSpan(2, 0, 0));
            var toDate = fromDate.AddMonths(1).AddSeconds(-1);

            var dbTimesheets = await _dbContext.Set<Timesheet>()
                .Where(x => x.StartTime >= fromDate.UtcDateTime && 
                            x.StartTime <= toDate.UtcDateTime && 
                            x.Status != TimesheetStatus.Confirmed
                ).ToListAsync();

            if (dbTimesheets.Count() == 0) return Unit.Value;

            foreach (var timesheet in dbTimesheets) timesheet.Status = TimesheetStatus.Confirmed;

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
