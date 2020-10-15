using BjBygg.Application.Application.Commands.DocumentTypeCommands;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.DocumentTypeTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class DeleteDocumentTypeTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidDocumentTypeId()
        {
            var command = new DeleteDocumentTypeCommand { Id = "notvalid" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }
        [Test]
        public async Task ShouldDeleteDocumentType()
        {
            await AddAsync(new DocumentType() { Id = "test", Name = "test435" });

            await SendAsync(new DeleteDocumentTypeCommand{Id = "test"});

            var type = await FindAsync<DocumentType>("test");

            type.Should().BeNull();
        }
    }
}
