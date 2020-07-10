using BjBygg.Application.Application.Commands.DocumentTypeCommands;
using CleanArchitecture.Core.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CommandTests.DocumentTypeTests
{
    public class DeleteRangeDocumentTypeTests : DeleteRangeTestBase<DocumentType>
    {
        [Test]
        public async Task ShouldDeleteDocumentTypes()
        {
            var request = new DeleteRangeDocumentTypeCommand() { Ids = new int[] { 1, 2, 3 } };
            await base.ShouldDeleteBaseEntities(request);
        }

        [Test]
        public void ShouldThrowEntityNotFoundExceptionIfNoDocumentTypeFound()
        {
            var request = new DeleteRangeDocumentTypeCommand() { Ids = new int[] { 45, 46 } };
            base.ShouldThrowEntityNotFoundExceptionIfNoEntitiesFound(request);
        }
    }
}
