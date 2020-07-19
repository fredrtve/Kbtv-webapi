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
        public void ShouldRequireValidTimesheetId()
        {
            var command = new DeleteTimesheetCommand { Id = 77 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTimesheetWithStatusNotOpenIfUserIsLeader()
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

            await SendAsync(new DeleteTimesheetCommand { Id = newTimesheet.Id });

            var dbTimesheet = await FindAsync<Timesheet>(newTimesheet.Id);

            dbTimesheet.Should().BeNull();
        }

        [Test]
        public async Task ShouldThrowBadRequestExceptionIfTimesheetStatusNotOpen()
        {
            await RunAsDefaultUserAsync(Roles.Employee);

            var newTimesheet = await SendAsync(new CreateTimesheetCommand()
            {
                MissionId = 1,
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });

            await RunAsDefaultUserAsync(Roles.Leader);

            await SendAsync(new UpdateTimesheetStatusCommand() { Id = newTimesheet.Id, Status = TimesheetStatus.Confirmed });

            await RunAsDefaultUserAsync(Roles.Employee);

            var command = new DeleteTimesheetCommand { Id = newTimesheet.Id };

            FluentActions.Invoking(() =>
              SendAsync(command)).Should().Throw<BadRequestException>();
        }

        [Test]
        public async Task ShouldDeleteTimesheetNotBelongingToUserIfUserIsLeader()
        {
            await RunAsDefaultUserAsync(Roles.Management);

            var newTimesheet = await SendAsync(new CreateTimesheetCommand()
            {
                MissionId = 1,
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });

            await RunAsDefaultUserAsync(Roles.Leader);

            await SendAsync(new DeleteTimesheetCommand { Id = newTimesheet.Id });

            var dbTimesheet = await FindAsync<Timesheet>(newTimesheet.Id);

            dbTimesheet.Should().BeNull();
        }

        [Test]
        public async Task ShouldThrowForbiddenExceptionIfTimesheetNotBelongingToUser()
        {
            await RunAsDefaultUserAsync(Roles.Management);

            var newTimesheet = await SendAsync(new CreateTimesheetCommand()
            {
                MissionId = 1,
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            }); 

            await RunAsDefaultUserAsync(Roles.Employee);

            var command = new DeleteTimesheetCommand { Id = newTimesheet.Id };

            FluentActions.Invoking(() =>
              SendAsync(command)).Should().Throw<ForbiddenException>();
        }

        [Test]
        public async Task ShouldDeleteTimesheet()
        {
            await RunAsDefaultUserAsync(Roles.Employee);

            var newTimesheet = await SendAsync(new CreateTimesheetCommand()
            {
                MissionId = 1,
                Comment = "test",
                StartTime = 111,
                EndTime = 112
            });

            await SendAsync(new DeleteTimesheetCommand { Id = newTimesheet.Id });

            var dbTimesheet = await FindAsync<Timesheet>(newTimesheet.Id);

            dbTimesheet.Should().BeNull();
        }
    }
}
