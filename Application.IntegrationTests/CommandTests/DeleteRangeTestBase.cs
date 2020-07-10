using BjBygg.Application.Common.BaseEntityCommands.DeleteRange;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.SharedKernel;
using FluentAssertions;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CommandTests
{
    using static Testing;
    public abstract class DeleteRangeTestBase<TEntity> : TestBase where TEntity : BaseEntity
    {

        public async Task ShouldDeleteBaseEntities(DeleteRangeCommand deleteRequest)
        {
            var deleteResult = await SendAsync(deleteRequest);

            var entitiesAfterDelete = await GetAllAsync<TEntity>();
            var idsNotDeleted = entitiesAfterDelete.Where(x => deleteRequest.Ids.Contains(x.Id));

            idsNotDeleted.Should().BeEmpty();
            deleteResult.Should().Be(Unit.Value);
        }

        public void ShouldThrowEntityNotFoundExceptionIfNoEntitiesFound<TResponse>(IRequest<TResponse> deleteRequest)
        {
            Func<Task> deleteFunction = async () => await SendAsync(deleteRequest);
            deleteFunction.Should().Throw<EntityNotFoundException>();
        }
    }
}
