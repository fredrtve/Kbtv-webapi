using BjBygg.Application.Application.Commands.DocumentTypeCommands.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.DocumentTypeTests
{
    using static AppTesting;

    public class UpdateDocumentTypeTests : AppTestBase
    {
        [Test]
        public void ShouldRequireValidDocumentTypeId()
        {
            var command = new UpdateDocumentTypeCommand
            {
                Id = 99,
                Name = "New Name"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateDocumentType()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new UpdateDocumentTypeCommand
            {
                Id = 1,
                Name = "Updated Name"
            };

            await SendAsync(command);

            var entity = await FindAsync<DocumentType>(1);

            entity.Name.Should().Be(command.Name);
            entity.UpdatedBy.Should().NotBeNull();
            entity.UpdatedBy.Should().Be(user.UserName);
            entity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
