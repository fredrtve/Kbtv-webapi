using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CommandTests
{
    using static Testing;

    public abstract class CreateTestBase<TEntity> : TestBase where TEntity : class
    {
        public async Task ShouldCreateEntity<TResponse>(IRequest<TResponse> request)
        {
            await SendAsync(request);

            var countOfEntitiesAfterCreation = (await GetAllAsync<TEntity>()).Count();

            var countOfNewEntities = countOfEntitiesAfterCreation - GetSeederCount()[typeof(TEntity)];

            countOfNewEntities.Should().Be(1);
        }

        public void ShouldThrowDbUpdateExceptionWhenEmptyCommand<TResponse>(IRequest<TResponse> request)
        {
            Func<Task> f = async () => await SendAsync(request);

            f.Should().Throw<DbUpdateException>();
        }
    }
}
