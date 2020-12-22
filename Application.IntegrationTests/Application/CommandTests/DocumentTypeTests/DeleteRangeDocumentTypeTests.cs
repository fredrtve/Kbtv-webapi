using BjBygg.Application.Application.Commands.DocumentTypeCommands;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.DocumentTypeTests
{
    using static AppTesting;
    public class DeleteRangeDocumentTypeTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidDocumentTypeId()
        {
            var command = new DeleteRangeDocumentTypeCommand { Ids = new string[] { "notvalid", "notvalid1" } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldDeleteDocumentTypes()
        {
            var ids = new string[] { "test", "test1" };
            await AddAsync(new DocumentType() { Id = ids[0], Name = "test1" });
            await AddAsync(new DocumentType() { Id = ids[1], Name = "test2" });

            await SendAsync(new DeleteRangeDocumentTypeCommand { Ids = ids });

            var typeCount = (await GetAllAsync<DocumentType>()).Count;

            typeCount.Should().Be(0);
        }
    }
}
