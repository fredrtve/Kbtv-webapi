﻿using BjBygg.Application.Common;
using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.DeleteRange;
using BjBygg.Application.Identity.Common.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Identity.Commands.InboundEmailPasswordTests
{
    using static IdentityTesting;
    public class DeleteRangeInboundEmailPasswordTests : IdentityTestBase
    {
        [Test]
        public async Task ShouldNotRequireValidInboundEmailPasswordId()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);
            var command = new DeleteRangeInboundEmailPasswordCommand { Ids = new string[] { "notvalid", "notvalid2" } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldDeleteInboundEmailPasswords()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);
            var ids = new string[] { "test", "test2" };

            await AddAsync(new InboundEmailPassword() { Id = ids[0], Password = "test435" });
            await AddAsync(new InboundEmailPassword() { Id = ids[1], Password = "test22435" });

            await SendAsync(new DeleteRangeInboundEmailPasswordCommand { Ids = ids });

            var types = (await GetAllAsync<InboundEmailPassword>())
                .Where(x => ids.Contains(x.Id));

            types.Should().BeEmpty();
        }
    }
}
