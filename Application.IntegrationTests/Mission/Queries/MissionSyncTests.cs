using CleanArchitecture.Application.IntegrationTests;
using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Queries.DbSyncQueries.DocumentTypeQuery;

namespace Application.IntegrationTests.Mission.Queries
{
    using static Testing;

    public class MissionSyncTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllMissions()
        {
            var query = new DocumentTypeSyncQuery();

            var result = await SendAsync(query);

            result.Entities.ToList().Should().HaveCount(7);
        }

        //[Test]
        //public async Task ShouldReturnAllListsAndItems()
        //{
        //    await AddAsync(new TodoList
        //    {
        //        Title = "Shopping",
        //        Items =
        //            {
        //                new TodoItem { Title = "Apples", Done = true },
        //                new TodoItem { Title = "Milk", Done = true },
        //                new TodoItem { Title = "Bread", Done = true },
        //                new TodoItem { Title = "Toilet paper" },
        //                new TodoItem { Title = "Pasta" },
        //                new TodoItem { Title = "Tissues" },
        //                new TodoItem { Title = "Tuna" }
        //            }
        //    });

        //    var query = new GetTodosQuery();

        //    var result = await SendAsync(query);

        //    result.Lists.Should().HaveCount(1);
        //    result.Lists.First().Items.Should().HaveCount(7);
        //}
    }
}
