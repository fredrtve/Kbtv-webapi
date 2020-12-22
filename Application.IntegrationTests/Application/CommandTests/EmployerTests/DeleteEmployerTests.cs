using BjBygg.Application.Application.Commands.EmployerCommands;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.EmployerTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class DeleteEmployerTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidEmployerId()
        {
            var command = new DeleteEmployerCommand { Id = "notvalid" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }
        [Test]
        public async Task ShouldDeleteEmployer()
        {
            await AddAsync(new Employer() { Id = "test", Name = "test435" });

            await SendAsync(new DeleteEmployerCommand { Id = "test" });

            var type = await FindAsync<Employer>("test");

            type.Should().BeNull();
        }
    }
}
