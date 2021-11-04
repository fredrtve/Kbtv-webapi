using BjBygg.Application.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Application.Commands.TimesheetCommands.CreateTimesheet;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core;
using BjBygg.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.TimesheetTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class CreateTimesheetTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateEmployerCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTimesheet()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var endDate = DateTimeHelper.Now();
            var totalHours = 4;

            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await AddAsync(new Activity() { Id = "test", Name = "test" });
            await AddAsync(new MissionActivity() { Id = "test", MissionId = "test", ActivityId = "test" });

            var command = new CreateTimesheetCommand()
            {
                Id = "test",
                MissionActivityId = "test",
                Comment = "test",
                StartTime = DateTimeHelper.ConvertDateToEpoch(endDate.AddHours(-totalHours)) * 1000,
                EndTime = DateTimeHelper.ConvertDateToEpoch(endDate) * 1000
            };

            await SendAsync(command);

            var dbEntity = await FindAsync<Timesheet>("test");

            dbEntity.Should().NotBeNull();
            dbEntity.Id.Should().Be(command.Id);
            dbEntity.Comment.Should().Be(command.Comment);
            dbEntity.StartTime.Should().BeCloseTo(endDate.AddHours(-totalHours), 1000);
            dbEntity.EndTime.Should().BeCloseTo(endDate, 1000);
            dbEntity.TotalHours.Should().Be(totalHours);
            dbEntity.UserName.Should().Be(user.UserName);
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }
    }
}
