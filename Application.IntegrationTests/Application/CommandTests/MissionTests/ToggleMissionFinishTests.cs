using BjBygg.Application.Application.Commands.MissionCommands.ToggleMissionFinish;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests
{
    using static AppTesting;
    public class ToggleMissionFinishTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidMissionId()
        {
            var command = new ToggleMissionFinishCommand{ Id = "notvalid" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldToggleMissionFinishedProperty()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var newMission = new Mission() { Id = "test", Address = "test", Finished = false };

            await AddAsync(newMission);

            var command = new ToggleMissionFinishCommand{ Id = "test" };

            await SendAsync(command);

            var updatedMission = await FindAsync<Mission>("test");

            updatedMission.Finished.Should().Be(!newMission.Finished);
            updatedMission.UpdatedBy.Should().NotBeNull();
            updatedMission.UpdatedBy.Should().Be(user.UserName);
            updatedMission.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
