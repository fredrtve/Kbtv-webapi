using BjBygg.Application.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatus;
using BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatusRange;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.TimesheetTests
{
    using static AppTesting;
    class UpdateTimesheetStatusRangeTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new UpdateTimesheetStatusRangeCommand{};

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldUpdateTimesheetStatuses()
        {
            await RunAsDefaultUserAsync(Roles.Leader);
            var command = new CreateTimesheetCommand()
            {
                MissionId = 1,
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            };

            var timesheet1 = await SendAsync(command);
            var timesheet2 = await SendAsync(command);

            await SendAsync(new UpdateTimesheetStatusRangeCommand
            {
                Ids = new int[] { timesheet1.Id, timesheet2.Id },
                Status = TimesheetStatus.Confirmed
            });

            var dbTimesheet1 = await FindAsync<Timesheet>(timesheet1.Id);
            var dbTimesheet2 = await FindAsync<Timesheet>(timesheet2.Id);

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
