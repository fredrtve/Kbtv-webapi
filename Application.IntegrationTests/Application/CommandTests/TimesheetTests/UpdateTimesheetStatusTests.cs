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
                Id = "notvalid",
                Status = TimesheetStatus.Confirmed
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTimesheetStatus()
        {
            await RunAsDefaultUserAsync(Roles.Leader);
            var command = new CreateTimesheetCommand()
            {
                Id = "test",
                MissionId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            };

            await SendAsync(command);

            await SendAsync(new UpdateTimesheetStatusCommand
            {
                Id = command.Id,
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
