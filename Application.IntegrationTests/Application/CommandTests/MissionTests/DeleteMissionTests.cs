using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.MissionCommands.Delete;
using BjBygg.Application.Common.Exceptions;
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
        public void ShouldRequireValidMissionId()
        {
            var command = new DeleteMissionCommand { Id = 77 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }
        [Test]
        public async Task ShouldDeleteMission()
        {
            await SendAsync(new DeleteMissionCommand { Id = 1 });

            var type = await FindAsync<Mission>(1);

            type.Should().BeNull();
        }
    }
}
