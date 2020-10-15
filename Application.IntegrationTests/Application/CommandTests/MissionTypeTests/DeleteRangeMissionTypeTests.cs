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
        public void ShouldNotRequireValidMissionTypeId()
        {
            var command = new DeleteRangeMissionTypeCommand { Ids = new string[] { "notvalid", "notvalid2" } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldDeleteMissionTypes()
        {
            var ids = new string[] { "test", "test2" };

            await AddAsync(new MissionType() { Id = ids[0], Name = "test435" });
            await AddAsync(new MissionType() { Id = ids[1], Name = "test435" });

            await SendAsync(new DeleteRangeMissionTypeCommand { Ids = ids });

            var types = (await GetAllAsync<MissionType>())
                .Where(x => ids.Contains(x.Id));

            types.Should().BeEmpty();
        }
    }
}
