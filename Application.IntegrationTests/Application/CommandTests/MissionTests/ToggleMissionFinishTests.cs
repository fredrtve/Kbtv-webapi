using BjBygg.Application.Application.Commands.MissionCommands.ToggleMissionFinish;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests
{
    using static AppTesting;
    public class ToggleMissionFinishTests : AppTestBase
    {
        [Test]
        public void ShouldRequireValidMissionId()
        {
            var command = new ToggleMissionFinishCommand{ Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldToggleMissionFinishedProperty()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var entityBefore = await FindAsync<Mission>(1);

            var command = new ToggleMissionFinishCommand{ Id = 1 };

            await SendAsync(command);

            var entityAfter = await FindAsync<Mission>(1);

            entityAfter.Finished.Should().Be(!entityBefore.Finished);
            entityAfter.UpdatedBy.Should().NotBeNull();
            entityAfter.UpdatedBy.Should().Be(user.UserName);
            entityAfter.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
