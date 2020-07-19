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
        public void ShouldRequireValidDocumentTypeId()
        {
            var command = new DeleteDocumentTypeCommand { Id = 77 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }
        [Test]
        public async Task ShouldDeleteDocumentType()
        {
            await SendAsync(new DeleteDocumentTypeCommand{Id = 1});

            var type = await FindAsync<DocumentType>(1);

            type.Should().BeNull();
        }
    }
}
