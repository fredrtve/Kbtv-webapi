using BjBygg.Application.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatus;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.TimesheetTests
{
    using static AppTesting;
    class UpdateTimesheetStatusTests : AppTestBase
    {
        [Test]
        public void ShouldRequireValidTimesheetId()
        {
            var command = new UpdateTimesheetStatusCommand
            {
                Id = 99,
                Status = TimesheetStatus.Confirmed
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTimesheetStatus()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var timesheet = await SendAsync(new CreateTimesheetCommand()
            {
                MissionId = 1,
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });

            timesheet = await SendAsync(new UpdateTimesheetStatusCommand
            {
                Id = timesheet.Id,
                Status = TimesheetStatus.Confirmed
            });

            var entity = await FindAsync<Timesheet>(timesheet.Id);

            entity.Should().NotBeNull();
            entity.Status.Should().Be(TimesheetStatus.Confirmed);
            entity.UpdatedBy.Should().NotBeNull();
            entity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
