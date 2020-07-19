using BjBygg.Application.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Application.Commands.TimesheetCommands.Update;
using BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatus;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.TimesheetTests
{
    using static AppTesting;

    public class UpdateTimesheetTests : AppTestBase
    {
        [Test]
        public void ShouldRequireValidTimesheetId()
        {
            var command = new UpdateTimesheetCommand
            {
                Id = 99
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldThrowBadRequestExceptionIfTimesheetStatusNotOpen()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var newTimesheet = await SendAsync(new CreateTimesheetCommand()
            {
                MissionId = 1,
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });

            await SendAsync(new UpdateTimesheetStatusCommand() { Id = newTimesheet.Id, Status = TimesheetStatus.Confirmed });

            var command = new UpdateTimesheetCommand { Id = newTimesheet.Id };

            FluentActions.Invoking(() =>
              SendAsync(command)).Should().Throw<BadRequestException>();
        }

        [Test]
        public async Task ShouldThrowForbiddenExceptionIfTimesheetNotBelongingToUser()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var newTimesheet = await SendAsync(new CreateTimesheetCommand()
            {
                MissionId = 1,
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });

            await RunAsDefaultUserAsync(Roles.Employee);

            var command = new UpdateTimesheetCommand { Id = newTimesheet.Id };

            FluentActions.Invoking(() =>
              SendAsync(command)).Should().Throw<ForbiddenException>();
        }

        [Test]
        public async Task ShouldUpdateTimesheet()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var endDate = DateTimeHelper.Now();
            var totalHours = 4;

            var command = new UpdateTimesheetCommand
            {
                Id = 1,
                Comment = "test",
                StartTime = DateTimeHelper.ConvertDateToEpoch(endDate.AddHours(-totalHours)),
                EndTime = DateTimeHelper.ConvertDateToEpoch(endDate)
            };

            await SendAsync(command);

            var entity = await FindAsync<Timesheet>(1);

            entity.Should().NotBeNull();
            entity.Comment.Should().Be(command.Comment);
            entity.StartTime.Should().BeCloseTo(endDate.AddHours(-totalHours), 1000);
            entity.EndTime.Should().BeCloseTo(endDate, 1000);
            entity.TotalHours.Should().Be(totalHours);
            entity.UpdatedBy.Should().NotBeNull();
            entity.UpdatedBy.Should().Be(user.UserName);
            entity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
