using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.MissionCommands.Notes;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
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
            await SendAsync(new DeleteMissionNoteCommand { Id = "test" });

            var entity = await FindAsync<MissionNote>("test");

            entity.Should().BeNull();
        }
    }
}
