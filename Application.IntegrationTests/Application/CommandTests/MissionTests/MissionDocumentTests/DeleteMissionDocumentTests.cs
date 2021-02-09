using BjBygg.Application.Application.Commands.MissionCommands.Documents;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionDocumentTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class DeleteMissionDocumentTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidMissionDocumentId()
        {
            var command = new DeleteMissionDocumentCommand { Id = "notvalid" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }
        [Test]
        public async Task ShouldDeleteMissionDocument()
        {
            await AddAsync(new Mission() { Id = "test", Address = "test435" });
            await AddAsync(new MissionDocument() { Id = "test", MissionId = "test", Name = "test", FileName = "test435" });

            await SendAsync(new DeleteMissionDocumentCommand { Id = "test" });

            var entity = await FindAsync<MissionDocument>("test");

            entity.Should().BeNull();
        }
    }
}
