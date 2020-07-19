using BjBygg.Application.Application.Commands.DocumentTypeCommands.Create;
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
    //5 x all entities, created 2 yrs apart
    public class CreateDocumentTypeTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateDocumentTypeCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateDocumentType()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new CreateDocumentTypeCommand() { Name = "Test" };

            var entity = await SendAsync(command);

            var dbEntity = await FindAsync<DocumentType>(entity.Id);

            dbEntity.Should().NotBeNull();
            dbEntity.Name.Should().Be(command.Name);
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }
    }
}
