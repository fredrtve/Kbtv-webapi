using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.MissionCommands.Documents;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionDocumentTests
{
    using static AppTesting;
    public class DeleteRangeMissionDocumentTests : AppTestBase
    {
        [Test]
        public void ShouldRequireAtleastOneValidMissionDocumentId()
        {
            var command = new DeleteRangeMissionDocumentCommand { Ids = new int[] { 45, 46 } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteMissionDocuments()
        {
            var ids = new int[] { 1, 2 };

            await SendAsync(new DeleteRangeMissionDocumentCommand { Ids = ids });

            var entities = (await GetAllAsync<MissionDocument>())
                .Where(x => ids.Contains(x.Id));

            entities.Should().BeEmpty();
        }
    }
}
