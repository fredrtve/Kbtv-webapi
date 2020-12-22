using BjBygg.Application.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Application.Queries.TimesheetQueries;
using BjBygg.Application.Common;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.QueryTests
{
    using static AppTesting;
    public class TimesheetQueryTests : AppTestBase
    {
        [Test]
        public async Task ShouldReturnAllTimesheetIfEmptyQuery()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await SendAsync(new CreateTimesheetCommand()
            {
                Id = "test",
                MissionId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });
            await SendAsync(new CreateTimesheetCommand()
            {
                Id = "test2",
                MissionId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });
            var timesheets = await SendAsync(new TimesheetQuery());
            timesheets.Should().HaveCount(2);
        }

        [Test]
        public async Task ShouldReturnAllTimesheetInGivenDateRange()
        {
            await RunAsDefaultUserAsync(Roles.Leader);
            var now = DateTimeHelper.Now();

            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await SendAsync(new CreateTimesheetCommand()
            {
                Id = "test",
                MissionId = "test",
                Comment = "test",
                StartTime = 123,
                EndTime = 155
            });

            await SendAsync(new CreateTimesheetCommand()
            {
                Id = "test2",
                MissionId = "test",
                Comment = "test",
                StartTime = DateTimeHelper.ConvertDateToEpoch(now.AddDays(-1).Date),
                EndTime = DateTimeHelper.ConvertDateToEpoch(now.AddDays(-1))
            });

            var query = new TimesheetQuery()
            {
                StartDate = DateTimeHelper.ConvertDateToEpoch(now.AddDays(-2).Date),
                EndDate = DateTimeHelper.ConvertDateToEpoch(now.Date),
            };

            var timesheets = await SendAsync(query);
            timesheets.Should().HaveCount(1);
        }

        [Test]
        public async Task ShouldReturnAllTimesheetForGivenUser()
        {
            await RunAsDefaultUserAsync(Roles.Management);

            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await SendAsync(new CreateTimesheetCommand()
            {
                Id = "test",
                MissionId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });

            var query = new TimesheetQuery()
            {
                UserName = Roles.Management
            };

            var timesheets = await SendAsync(query);
            timesheets.Should().HaveCount(1);
        }

        [Test]
        public async Task ShouldReturnAllTimesheetForGivenMission()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await SendAsync(new CreateTimesheetCommand()
            {
                Id = "test",
                MissionId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });

            var query = new TimesheetQuery()
            {
                MissionId = "test"
            };

            var timesheets = await SendAsync(query);
            timesheets.Should().HaveCount(1);
        }
    }
}
