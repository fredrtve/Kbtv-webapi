using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.MissionCommands.Documents;
using BjBygg.Application.Common.Exceptions;
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
        public void ShouldRequireValidMissionDocumentId()
        {
            var command = new DeleteMissionDocumentCommand { Id = 77 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }
        [Test]
        public async Task ShouldDeleteMissionDocument()
        {
            await SendAsync(new DeleteMissionDocumentCommand { Id = 1 });

            var entity = await FindAsync<MissionDocument>(1);

            entity.Should().BeNull();
        }
    }
}
