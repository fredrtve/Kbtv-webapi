using BjBygg.Application.Application.Commands.DocumentTypeCommands.Update;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CommandTests.DocumentTypeTests
{
    using static Testing;

    public class UpdateDocumentTypeTests : UpdateTestBase<DocumentType>
    {
        [Test]
        public async Task ShouldUpdateDocumentType()
        {
            var command = new UpdateDocumentTypeCommand() { Id = 3, Name = "updated" };
            await SendAsync(command);
            var updatedEntity = await FindAsync<DocumentType>(command.Id);
            updatedEntity.Name.Should().Be(command.Name);
            updatedEntity.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, 50);
        }

        [Test]
        public void ShouldThrowEntityNotFoundExceptionIfNoDocumentTypeFound()
        {
            base.ShouldThrowEntityNotFoundExceptionIfNoEntityFound(new UpdateDocumentTypeCommand() { Id = 70 });
        }
    }
}
