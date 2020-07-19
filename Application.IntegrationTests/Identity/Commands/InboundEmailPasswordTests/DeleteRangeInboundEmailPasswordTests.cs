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
        public void ShouldRequireAtleastOneValidInboundEmailPasswordId()
        {
            var command = new DeleteRangeInboundEmailPasswordCommand { Ids = new int[] { 45, 46 } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteInboundEmailPasswords()
        {
            var pw1 = await SendAsync(new CreateInboundEmailPasswordCommand() { Password = "Test1" });
            var pw2 = await SendAsync(new CreateInboundEmailPasswordCommand() { Password = "Test2" });

            var ids = new int[] { pw1.Id, pw2.Id };

            await SendAsync(new DeleteRangeInboundEmailPasswordCommand { Ids = ids });

            var types = (await GetAllAsync<InboundEmailPassword>())
                .Where(x => ids.Contains(x.Id));

            types.Should().BeEmpty();
        }
    }
}
