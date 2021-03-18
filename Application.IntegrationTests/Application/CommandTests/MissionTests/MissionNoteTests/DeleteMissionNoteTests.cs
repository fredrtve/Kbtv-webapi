using BjBygg.Application.Application.Commands.MissionCommands.Notes;
using BjBygg.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionNoteTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class DeleteMissionNoteTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidEmployerId()
        {
            var command = new DeleteMissionNoteCommand { Id = "notvalid" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }
        [Test]
        public async Task ShouldDeleteEmployer()
        {
            await AddAsync(new Mission() { Id = "test", Address = "test435" });
            await AddAsync(new MissionNote() { Id = "test", MissionId = "test", Content = "test435" });

            await SendAsync(new DeleteMissionNoteCommand { Id = "test" });

            var entity = await FindAsync<MissionNote>("test");

            entity.Should().BeNull();
        }
    }
}
