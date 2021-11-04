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
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.TimesheetTests
{
    using static AppTesting;

    public class UpdateTimesheetTests : AppTestBase
    {
        [Test]
        public async Task ShouldRequireValidTimesheetId()
        {
            await AddAsync(new Activity() { Id = "test", Name = "test" });
            await AddAsync(new MissionActivity() { Id = "test", MissionId = "test", ActivityId = "test" });
            var command = new UpdateTimesheetCommand
            {
                Id = "notvalid",
                MissionActivityId = "test",
                StartTime = 1231131,
                EndTime = 2342342,
                Comment = "asddsada"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldThrowBadRequestExceptionIfTimesheetStatusNotOpen()
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

            await SendAsync(new UpdateTimesheetStatusRangeCommand() { Ids = new[] { command.Id }, Status = TimesheetStatus.Confirmed });

            var updateCommand = new UpdateTimesheetCommand { Id = command.Id, MissionActivityId = "test", StartTime = 1231131, EndTime = 2342342, Comment = "asddsada" };

            FluentActions.Invoking(() =>
              SendAsync(updateCommand)).Should().Throw<BadRequestException>();
        }

        [Test]
        public async Task ShouldThrowForbiddenExceptionIfTimesheetNotBelongingToUser()
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

            await RunAsDefaultUserAsync(Roles.Employee);

            var updateCommand = new UpdateTimesheetCommand { Id = command.Id, MissionActivityId = "test", StartTime = 1231131, EndTime = 2342342, Comment = "asddsada" };

            FluentActions.Invoking(() =>
              SendAsync(updateCommand)).Should().Throw<ForbiddenException>();
        }

        [Test]
        public async Task ShouldUpdateTimesheet()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);
            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await AddAsync(new Activity() { Id = "test", Name = "test" });
            await AddAsync(new MissionActivity() { Id = "test", MissionId = "test", ActivityId = "test" });
            await SendAsync(new CreateTimesheetCommand()
            {
                Id = "test",
                MissionActivityId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });

            var endDate = DateTimeHelper.Now();
            var totalHours = 4;

            var command = new UpdateTimesheetCommand
            {
                Id = "test",
                Comment = "test2",
                MissionActivityId = "test",
                StartTime = DateTimeHelper.ConvertDateToEpoch(endDate.AddHours(-totalHours)) * 1000,
                EndTime = DateTimeHelper.ConvertDateToEpoch(endDate) * 1000
            };

            await SendAsync(command);

            var entity = await FindAsync<Timesheet>("test");

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
