using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.EmployerTests
{
    using static AppTesting;
    public class DeleteRangeEmployerTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidEmployerId()
        {
            var command = new DeleteRangeEmployerCommand { Ids = new string[] { "notvalid", "notvalid1" } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldDeleteEmployers()
        {
            var ids = new string[] { "test", "test1" };
            await AddAsync(new Employer() { Id = ids[0], Name = "test435" });
            await AddAsync(new Employer() { Id = ids[1], Name = "test435" });
            await SendAsync(new DeleteRangeEmployerCommand { Ids = ids });

            var types = (await GetAllAsync<Employer>())
                .Where(x => ids.Contains(x.Id));

            types.Should().BeEmpty();
        }
    }
}
