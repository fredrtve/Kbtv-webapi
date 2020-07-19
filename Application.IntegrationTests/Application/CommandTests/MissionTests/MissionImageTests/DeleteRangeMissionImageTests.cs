using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.MissionCommands.Images;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionImageTests
{
    using static AppTesting;
    public class DeleteRangeMissionImageTests : AppTestBase
    {
        [Test]
        public void ShouldRequireAtleastOneValidMissionImageId()
        {
            var command = new DeleteRangeMissionImageCommand { Ids = new int[] { 45, 46 } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteMissionImages()
        {
            var ids = new int[] { 1, 2 };

            await SendAsync(new DeleteRangeMissionImageCommand { Ids = ids });

            var entities = (await GetAllAsync<MissionImage>())
                .Where(x => ids.Contains(x.Id));

            entities.Should().BeEmpty();
        }
    }
}
