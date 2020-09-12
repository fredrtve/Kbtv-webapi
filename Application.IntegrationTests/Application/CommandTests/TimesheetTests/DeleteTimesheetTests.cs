using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Application.Commands.TimesheetCommands.Delete;
using BjBygg.Application.Application.Commands.TimesheetCommands.UpdateStatus;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.TimesheetTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class DeleteTimesheetTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidTimesheetId()
        {
            var command = new DeleteTimesheetCommand { Id = "notvalid" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldDeleteTimesheetWithStatusNotOpenIfUserIsLeader()
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

            await SendAsync(new UpdateTimesheetStatusCommand() { Id = command.Id, Status = TimesheetStatus.Confirmed });

            await SendAsync(new DeleteTimesheetCommand { Id = command.Id });

            var dbTimesheet = await FindAsync<Timesheet>(command.Id);

            dbTimesheet.Should().BeNull();
        }

        [Test]
        public async Task ShouldThrowBadRequestExceptionIfTimesheetStatusNotOpen()
        {
            await RunAsDefaultUserAsync(Roles.Employee);

            var command = new CreateTimesheetCommand()
            {
                Id = "test",
                MissionId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            };

            await SendAsync(command);

            await RunAsDefaultUserAsync(Roles.Leader);

            await SendAsync(new UpdateTimesheetStatusCommand() { Id = command.Id, Status = TimesheetStatus.Confirmed });

            await RunAsDefaultUserAsync(Roles.Employee);

            var deleteCommand = new DeleteTimesheetCommand { Id = command.Id };

            FluentActions.Invoking(() =>
              SendAsync(deleteCommand)).Should().Throw<BadRequestException>();
        }

        [Test]
        public async Task ShouldDeleteTimesheetNotBelongingToUserIfUserIsLeader()
        {
            await RunAsDefaultUserAsync(Roles.Management);
            var command = new CreateTimesheetCommand()
            {
                Id = "test",
                MissionId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            };

            await SendAsync(command);

            await RunAsDefaultUserAsync(Roles.Leader);

            await SendAsync(new DeleteTimesheetCommand { Id = command.Id });

            var dbTimesheet = await FindAsync<Timesheet>(command.Id);

            dbTimesheet.Should().BeNull();
        }

        [Test]
        public async Task ShouldThrowForbiddenExceptionIfTimesheetNotBelongingToUser()
        {
            await RunAsDefaultUserAsync(Roles.Management);
            var command = new CreateTimesheetCommand()
            {
                Id = "test",
                MissionId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            };
            await SendAsync(command); 

            await RunAsDefaultUserAsync(Roles.Employee);

            var deleteCommand = new DeleteTimesheetCommand { Id = command.Id };

            FluentActions.Invoking(() =>
              SendAsync(deleteCommand)).Should().Throw<ForbiddenException>();
        }

        [Test]
        public async Task ShouldDeleteTimesheet()
        {
            await RunAsDefaultUserAsync(Roles.Employee);
            var command = new CreateTimesheetCommand()
            {
                Id = "test",
                MissionId = "test",
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            };

            await SendAsync(command);

            await SendAsync(new DeleteTimesheetCommand { Id = command.Id });

            var dbTimesheet = await FindAsync<Timesheet>(command.Id);

            dbTimesheet.Should().BeNull();
        }
    }
}
