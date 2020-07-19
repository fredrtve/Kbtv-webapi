using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Common.Exceptions;
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
        public void ShouldRequireValidEmployerId()
        {
            var command = new DeleteEmployerCommand { Id = 77 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }
        [Test]
        public async Task ShouldDeleteEmployer()
        {
            await SendAsync(new DeleteEmployerCommand { Id = 1 });

            var entity = await FindAsync<Employer>(1);

            entity.Should().BeNull();
        }
    }
}
