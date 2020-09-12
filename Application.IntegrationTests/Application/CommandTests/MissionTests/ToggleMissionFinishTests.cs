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
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldToggleMissionFinishedProperty()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var entityBefore = await FindAsync<Mission>("test");

            var command = new ToggleMissionFinishCommand{ Id = "test" };

            await SendAsync(command);

            var entityAfter = await FindAsync<Mission>("test");

            entityAfter.Finished.Should().Be(!entityBefore.Finished);
            entityAfter.UpdatedBy.Should().NotBeNull();
            entityAfter.UpdatedBy.Should().Be(user.UserName);
            entityAfter.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
