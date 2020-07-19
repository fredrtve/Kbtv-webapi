using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.MissionTypeCommands;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTypeTests
{
    using static AppTesting;
    public class DeleteRangeMissionTypeTests : AppTestBase
    {
        [Test]
        public void ShouldRequireAtleastOneValidMissionTypeId()
        {
            var command = new DeleteRangeMissionTypeCommand { Ids = new int[] { 45, 46 } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteMissionTypes()
        {
            var ids = new int[] { 1, 2 };

            await SendAsync(new DeleteRangeMissionTypeCommand { Ids = ids });

            var types = (await GetAllAsync<MissionType>())
                .Where(x => ids.Contains(x.Id));

            types.Should().BeEmpty();
        }
    }
}
