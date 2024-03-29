﻿using BjBygg.Application.Application.Commands.MissionCommands.DeleteRange;
using BjBygg.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests
{
    using static AppTesting;
    public class DeleteRangeMissionTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidMissionId()
        {
            var command = new DeleteRangeMissionCommand { Ids = new string[] { "notvalid", "notvalid2" } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldDeleteMissions()
        {
            var ids = new string[] { "test", "test2" };

            await AddAsync(new Mission() { Id = ids[0], Address = "test435" });
            await AddAsync(new Mission() { Id = ids[1], Address = "test435" });

            await SendAsync(new DeleteRangeMissionCommand { Ids = ids });

            var types = (await GetAllAsync<Mission>())
                .Where(x => ids.Contains(x.Id));

            types.Should().BeEmpty();
        }
    }
}
