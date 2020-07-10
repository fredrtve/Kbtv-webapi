using BjBygg.Application.Application.Commands.EmployerCommands;
using CleanArchitecture.Core.Entities;
using NUnit.Framework;

namespace Application.IntegrationTests.CommandTests.EmployerTests
{
    //5 x all entities, created 2 yrs apart
    public class DeleteEmployerTests : DeleteTestBase<Employer>
    {
        [Test]
        public void ShouldDeleteEmployer()
        {
            base.ShouldDeleteBaseEntity(new DeleteEmployerCommand() { Id = 1 });
        }

        [Test]
        public void ShouldThrowEntityNotFoundExceptionIfNoEmployerFound()
        {
            base.ShouldThrowEntityNotFoundExceptionIfNoEntityFound(new DeleteEmployerCommand() { Id = 70 });
        }
    }
}
