﻿using BjBygg.Application.Application.Commands.TimesheetCommands.CreateTimesheet;
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
    class UpdateTimesheetStatusTests : AppTestBase
    {
        [Test]
        public void ShouldRequireValidTimesheetId()
        {
            var command = new UpdateTimesheetStatusRangeCommand
            {
                Ids = new[] { "notvalid" },
                Status = TimesheetStatus.Confirmed
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTimesheetStatus()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await AddAsync(new Activity() { Id = "test", Name = "test" });
            await AddAsync(new MissionActivity() { Id = "test", MissionId = "test", ActivityId = "test" });

            var command = new CreateTimesheetCommand()
            {
                Id = "test",
                MissionActivityId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            };

            await SendAsync(command);

            await SendAsync(new UpdateTimesheetStatusRangeCommand
            {
                Ids = new[] { command.Id },
                Status = TimesheetStatus.Confirmed
            });

            var entity = await FindAsync<Timesheet>(command.Id);

            entity.Should().NotBeNull();
            entity.Status.Should().Be(TimesheetStatus.Confirmed);
            entity.UpdatedBy.Should().NotBeNull();
            entity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
