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
        public void ShouldRequireValidMissionTypeId()
        {
            var command = new DeleteMissionTypeCommand { Id = 77 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }
        [Test]
        public async Task ShouldDeleteMissionType()
        {
            await SendAsync(new DeleteMissionTypeCommand { Id = 1 });

            var type = await FindAsync<MissionType>(1);

            type.Should().BeNull();
        }
    }
}
