using BjBygg.Application.Application.Commands.MissionCommands.Delete;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class DeleteMissionTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidMissionId()
        {
            var command = new DeleteMissionCommand { Id = "notvalid" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }
        [Test]
        public async Task ShouldDeleteMission()
        {
            await AddAsync(new Mission() { Id = "test", Address = "testaddress23" });

            await SendAsync(new DeleteMissionCommand { Id = "test" });

            var type = await FindAsync<Mission>("test");

            type.Should().BeNull();
        }
    }
}
