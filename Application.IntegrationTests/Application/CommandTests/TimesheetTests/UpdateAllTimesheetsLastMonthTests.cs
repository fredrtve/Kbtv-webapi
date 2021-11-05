using BjBygg.Application.Application.Commands.TimesheetCommands;
using BjBygg.Application.Application.Commands.TimesheetCommands.CreateTimesheet;
using BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatusRange;
using BjBygg.Application.Application.Commands.TimesheetCommands.UpdateTimesheet;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.Core.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.TimesheetTests
{
    using static AppTesting;

    public class UpdateAllTimesheetsLastMonthTests : AppTestBase
    {
        [Test]
        public async Task ShouldNotUpdateIfConfirmTimesheetsMonthlyIsFalse()
        {
            await RunAsDefaultUserAsync(Roles.Leader);
            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await AddAsync(new LeaderSettings() { Id = "test", ConfirmTimesheetsMonthly = false });

            var lastMonth = DateTimeHelper.NowLocalTime().AddMonths(-1);
            var firstDayMonth = new DateTimeOffset(lastMonth.Year, lastMonth.Month, 1, 0, 0, 0, new TimeSpan(2, 0, 0));

            await CreateTimesheet("test1", firstDayMonth.DateTime + new TimeSpan(15, 12, 0, 0), new TimeSpan(2, 0, 0)); //15st day @ 12:00-14:00 (+2)

            await SendAsync(new ConfirmAllTimesheetsLastMonthCommand());

            var test1 = await FindAsync<Timesheet>("test1");

            test1.Status.Should().Be(TimesheetStatus.Open);
        }

        [Test]
        public async Task ShouldConfirmAllTimesheetsLastMonth()
        {
            await RunAsDefaultUserAsync(Roles.Leader);
            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await AddAsync(new LeaderSettings() { Id = "test", ConfirmTimesheetsMonthly = true });
            await AddAsync(new Activity() { Id = "test", Name = "test" });
            await AddAsync(new MissionActivity() { Id = "test", MissionId = "test", ActivityId = "test" });

            var lastMonth = DateTimeHelper.NowLocalTime().AddMonths(-1);
            var firstDayMonth = new DateTimeOffset(lastMonth.Year, lastMonth.Month, 1, 0, 0, 0, new TimeSpan(2, 0, 0));

            await CreateTimesheet("test1", firstDayMonth.DateTime, new TimeSpan(2, 0, 0)); //1st day @ 00:00-02:00 (+2)

            await CreateTimesheet("test2", firstDayMonth.DateTime + new TimeSpan(15, 12, 0, 0), new TimeSpan(2, 0, 0)); //15st day @ 12:00-14:00 (+2)

            await CreateTimesheet("test3", firstDayMonth.DateTime + new TimeSpan(5, 14, 0, 0), new TimeSpan(2, 0, 0)); //5st day @ 14:00-16:00 (+2)

            await CreateTimesheet("test4", firstDayMonth.DateTime.AddMonths(1).AddHours(-2), new TimeSpan(0, 59, 0)); //Last day @ 23:00-23:59 (+2)

            await SendAsync(new ConfirmAllTimesheetsLastMonthCommand());

            var test1 = await FindAsync<Timesheet>("test1");
            var test2 = await FindAsync<Timesheet>("test2");
            var test3 = await FindAsync<Timesheet>("test3");
            var test4 = await FindAsync<Timesheet>("test4");

            test1.Status.Should().Be(TimesheetStatus.Confirmed);
            test2.Status.Should().Be(TimesheetStatus.Confirmed);
            test3.Status.Should().Be(TimesheetStatus.Confirmed);
            test4.Status.Should().Be(TimesheetStatus.Confirmed);
        }

        [Test]
        public async Task ShouldNotConfirmTimesheetsInMonthBefore()
        {
            await RunAsDefaultUserAsync(Roles.Leader);
            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await AddAsync(new LeaderSettings() { Id = "test", ConfirmTimesheetsMonthly = true });

            var lastMonth = DateTimeHelper.NowLocalTime().AddMonths(-1);
            var firstDayMonth = new DateTimeOffset(lastMonth.Year, lastMonth.Month, 1, 0, 0, 0, new TimeSpan(2, 0, 0));

            await CreateTimesheet("test1", firstDayMonth.AddHours(-2).DateTime, new TimeSpan(1, 59, 59)); //Last day of prev month @ 22:00-23:59 (+2)

            var test1 = await FindAsync<Timesheet>("test1");

            test1.Status.Should().Be(TimesheetStatus.Open);
        }

        [Test]
        public async Task ShouldNotConfirmTimesheetsInMonthAfter()
        {
            await RunAsDefaultUserAsync(Roles.Leader);
            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await AddAsync(new LeaderSettings() { Id = "test", ConfirmTimesheetsMonthly = true });

            var lastMonth = DateTimeHelper.NowLocalTime().AddMonths(-1);
            var firstDayMonth = new DateTimeOffset(lastMonth.Year, lastMonth.Month, 1, 0, 0, 0, new TimeSpan(2, 0, 0));

            await CreateTimesheet("test1", firstDayMonth.AddMonths(1).DateTime, new TimeSpan(2, 0, 0)); //First day of next month @ 00:00-02:00 (+2)

            var test1 = await FindAsync<Timesheet>("test1");

            test1.Status.Should().Be(TimesheetStatus.Open);
        }


        private async Task CreateTimesheet(string id, DateTime date, TimeSpan length)
        {
            await SendAsync(new CreateTimesheetCommand()
            {
                Id = id,
                MissionActivityId = "test",
                Comment = "test",
                StartTime = DateTimeHelper.ConvertDateToEpoch(date) * 1000,
                EndTime = DateTimeHelper.ConvertDateToEpoch(date + length) * 1000,
            });
        }
    }
}
