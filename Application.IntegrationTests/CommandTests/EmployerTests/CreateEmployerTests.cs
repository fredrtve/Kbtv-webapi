using BjBygg.Application.Application.Commands.EmployerCommands.Create;
using CleanArchitecture.Core.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CommandTests.EmployerTests
{
    //5 x all entities, created 2 yrs apart
    public class CreateEmployerTests : CreateTestBase<Employer>
    {
        [Test]
        public async Task ShouldCreateEmployer()
        {
            var command = new CreateEmployerCommand() { Name = "Test" };
            await base.ShouldCreateEntity(command);
        }

        [Test]
        public void ShouldThrowDbUpdateExceptionWhenEmptyCommand()
        {
            var command = new CreateEmployerCommand() { };
            base.ShouldThrowDbUpdateExceptionWhenEmptyCommand(command);
        }
    }
}
