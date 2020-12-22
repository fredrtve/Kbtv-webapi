using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.MissionCommands.Images;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionImageTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class DeleteMissionImageTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidMissionImageId()
        {
            var command = new DeleteEmployerCommand { Id = "notvalid" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldDeleteMissionImage()
        {
            await AddAsync(new Mission() { Id = "test", Address = "test435" });
            await AddAsync(new MissionImage() { Id = "test", MissionId = "test", FileName = "test435" });

            await SendAsync(new DeleteMissionImageCommand { Id = "test" });

            var entity = await FindAsync<MissionImage>("test");

            entity.Should().BeNull();
        }
    }
}
