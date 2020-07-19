using BjBygg.Application.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Application.Commands.TimesheetCommands.Create;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
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
            
            var command = new CreateTimesheetCommand() { 
                MissionId = 1, 
                Comment = "test", 
                StartTime = DateTimeHelper.ConvertDateToEpoch(endDate.AddHours(-totalHours)),
                EndTime = DateTimeHelper.ConvertDateToEpoch(endDate)
            };

            var entity = await SendAsync(command);

            var dbEntity = await FindAsync<Timesheet>(entity.Id);

            dbEntity.Should().NotBeNull();
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
