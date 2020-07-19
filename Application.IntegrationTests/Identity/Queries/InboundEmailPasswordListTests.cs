using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Create;
using BjBygg.Application.Identity.Queries;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Queries
{
    using static IdentityTesting;
    public class InboundEmailPasswordListTests : IdentityTestBase
    {
        [Test]
        public async Task ShouldReturnAllInboundEmailPasswords()
        {
            await SendAsync(new CreateInboundEmailPasswordCommand{ Password = "test1234" });
            await SendAsync(new CreateInboundEmailPasswordCommand{ Password = "test2454" });

            var result = await SendAsync(new InboundEmailPasswordListQuery());

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}
