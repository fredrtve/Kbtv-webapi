using BjBygg.Application.Application.Commands.DocumentTypeCommands;
using CleanArchitecture.Core.Entities;
using NUnit.Framework;

namespace Application.IntegrationTests.CommandTests.DocumentTypeTests
{
    //5 x all entities, created 2 yrs apart
    public class DeleteDocumentTypeTests : DeleteTestBase<DocumentType>
    {
        [Test]
        public void ShouldDeleteDocumentType()
        {
            base.ShouldDeleteBaseEntity(new DeleteDocumentTypeCommand() { Id = 1 });
        }

        [Test]
        public void ShouldThrowEntityNotFoundExceptionIfNoDocumentTypeFound()
        {
            base.ShouldThrowEntityNotFoundExceptionIfNoEntityFound(new DeleteDocumentTypeCommand() { Id = 70 });
        }
    }
}
