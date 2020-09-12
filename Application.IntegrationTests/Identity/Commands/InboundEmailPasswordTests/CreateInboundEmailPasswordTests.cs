using BjBygg.Application.Application.Commands.DocumentTypeCommands.Create;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Create;
using BjBygg.Application.Identity.Common.Models;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.InboundEmailPasswordTests
{
    using static IdentityTesting;
    //5 x all entities, created 2 yrs apart
    public class CreateInboundEmailPasswordTests : IdentityTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateInboundEmailPasswordCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateInboundEmailPassword()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new CreateInboundEmailPasswordCommand() { Id = "test", Password = "Test" };

            await SendAsync(command);
            
            var dbEntity = await FindAsync<InboundEmailPassword>(command.Id);

            dbEntity.Should().NotBeNull();
            dbEntity.Password.Should().Be(command.Password);
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }
    }
}
