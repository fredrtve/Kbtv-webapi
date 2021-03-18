using BjBygg.Application.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatusRange;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.Core.Enums;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.TimesheetTests
{
    using static AppTesting;
    class UpdateTimesheetStatusRangeTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new UpdateTimesheetStatusRangeCommand { };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldUpdateTimesheetStatuses()
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

            await SendAsync(new UpdateTimesheetStatusRangeCommand
            {
                Ids = new string[] { "test", "test2" },
                Status = TimesheetStatus.Confirmed
            });

            var dbTimesheet1 = await FindAsync<Timesheet>("test");
            var dbTimesheet2 = await FindAsync<Timesheet>("test2");

            dbTimesheet1.Should().NotBeNull();
            dbTimesheet1.Status.Should().Be(TimesheetStatus.Confirmed);
            dbTimesheet1.UpdatedBy.Should().Be(Roles.Leader);
            dbTimesheet1.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);

            dbTimesheet2.Should().NotBeNull();
            dbTimesheet2.Status.Should().Be(TimesheetStatus.Confirmed);
            dbTimesheet2.UpdatedBy.Should().Be(Roles.Leader);
            dbTimesheet2.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
