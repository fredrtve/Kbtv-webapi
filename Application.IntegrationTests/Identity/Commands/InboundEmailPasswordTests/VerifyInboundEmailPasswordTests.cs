using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Create;
using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Verify;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.InboundEmailPasswordTests
{
    using static IdentityTesting;
    public class VerifyInboundEmailPasswordTests : IdentityTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new VerifyInboundEmailPasswordCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldReturnTrueIfPasswordMatchAnyInboundEmailPassword()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var createCommand = new CreateInboundEmailPasswordCommand() { Id = "test", Password = "Test" };

            await SendAsync(createCommand);

            var result = 
                await SendAsync(new VerifyInboundEmailPasswordCommand() { Password = createCommand.Password });

            result.Should().BeTrue();
        }

        [Test]
        public async Task ShouldReturnFalseIfPasswordDoesNotMatchAnyInboundEmailPassword()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            await SendAsync(new CreateInboundEmailPasswordCommand() { Id = "test", Password = "Test" });

            var result =
                await SendAsync(new VerifyInboundEmailPasswordCommand() { Password = "Test2" });

            result.Should().BeFalse();
        }
    }
}
