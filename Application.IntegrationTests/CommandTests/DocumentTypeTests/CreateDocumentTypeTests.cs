using BjBygg.Application.Application.Commands.DocumentTypeCommands.Create;
using CleanArchitecture.Core.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CommandTests.DocumentTypeTests
{
    //5 x all entities, created 2 yrs apart
    public class CreateDocumentTypeTests : CreateTestBase<DocumentType>
    {
        [Test]
        public async Task ShouldCreateDocumentType()
        {
            var command = new CreateDocumentTypeCommand() { Name = "Test" };
            await base.ShouldCreateEntity(command);
        }

        [Test]
        public void ShouldThrowDbUpdateExceptionWhenEmptyCommand()
        {
            var command = new CreateDocumentTypeCommand() { };
            base.ShouldThrowDbUpdateExceptionWhenEmptyCommand(command);
        }
    }
}
