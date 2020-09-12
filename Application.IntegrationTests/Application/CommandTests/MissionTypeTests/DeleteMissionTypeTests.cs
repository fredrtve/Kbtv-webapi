using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.MissionTypeCommands;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTypeTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class DeleteMissionTypeTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidMissionTypeId()
        {
            var command = new DeleteMissionTypeCommand { Id = "notvalid" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }
        [Test]
        public async Task ShouldDeleteMissionType()
        {
            await SendAsync(new DeleteMissionTypeCommand { Id = "test" });

            var type = await FindAsync<MissionType>("test");

            type.Should().BeNull();
        }
    }
}
