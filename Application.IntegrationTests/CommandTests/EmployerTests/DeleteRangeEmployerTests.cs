using BjBygg.Application.Application.Commands.EmployerCommands;
using CleanArchitecture.Core.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CommandTests.EmployerTests
{
    public class DeleteRangeEmployerTests : DeleteRangeTestBase<Employer>
    {
        [Test]
        public async Task ShouldDeleteEmployers()
        {
            var request = new DeleteRangeEmployerCommand() { Ids = new int[] { 1, 2, 3 } };
            await base.ShouldDeleteBaseEntities(request);
        }

        [Test]
        public void ShouldThrowEntityNotFoundExceptionIfNoEmployerFound()
        {
            var request = new DeleteRangeEmployerCommand() { Ids = new int[] { 45, 46 } };
            base.ShouldThrowEntityNotFoundExceptionIfNoEntitiesFound(request);
        }
    }
}
