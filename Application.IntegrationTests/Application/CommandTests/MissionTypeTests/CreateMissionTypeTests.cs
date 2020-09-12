using BjBygg.Application.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Application.Commands.MissionTypeCommands.Create;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTypeTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class CreateMissionTypeTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateMissionTypeCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateMissionType()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new CreateMissionTypeCommand() { Id = "test", Name = "Test" };

            await SendAsync(command);

            var dbEntity = await FindAsync<MissionType>(command.Id);

            dbEntity.Should().NotBeNull();
            dbEntity.Name.Should().Be(command.Name);
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }
    }
}
