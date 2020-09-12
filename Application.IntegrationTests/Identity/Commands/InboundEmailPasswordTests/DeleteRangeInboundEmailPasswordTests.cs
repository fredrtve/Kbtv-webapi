using BjBygg.Application.Application.Commands.DocumentTypeCommands;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Create;
using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.DeleteRange;
using BjBygg.Application.Identity.Common.Models;
using CleanArchitecture.Core.Entities;
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
        public void ShouldNotRequireValidInboundEmailPasswordId()
        {
            var command = new DeleteRangeInboundEmailPasswordCommand { Ids = new string[] { "notvalid", "notvalid2" } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldDeleteInboundEmailPasswords()
        {
            var ids = new string[] { "test", "test2" };

            await SendAsync(new DeleteRangeInboundEmailPasswordCommand { Ids = ids });

            var types = (await GetAllAsync<InboundEmailPassword>())
                .Where(x => ids.Contains(x.Id));

            types.Should().BeEmpty();
        }
    }
}
