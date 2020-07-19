using BjBygg.Application.Application.Commands.DocumentTypeCommands;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.DocumentTypeTests
{
    using static AppTesting;
    public class DeleteRangeDocumentTypeTests : AppTestBase
    {
        [Test]
        public void ShouldRequireAtleastOneValidDocumentTypeId()
        {
            var command = new DeleteRangeDocumentTypeCommand { Ids = new int[] { 45, 46 } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteDocumentTypes()
        {       
            var ids = new int[] { 1, 2 };

            await SendAsync(new DeleteRangeDocumentTypeCommand { Ids = ids });

            var types = (await GetAllAsync<DocumentType>())
                .Where(x => ids.Contains(x.Id));

            types.Should().BeEmpty();
        }
    }
}
