using BjBygg.Application.Common.BaseEntityCommands.Delete;
using BjBygg.Application.Common.Exceptions;
using FluentAssertions;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CommandTests
{
    using static Testing;

    public abstract class DeleteTestBase<TEntity> : TestBase where TEntity : class
    {

        public async void ShouldDeleteBaseEntity(DeleteCommand deleteRequest)
        {
            var deleteResult = await SendAsync(deleteRequest);
            Func<Task> findFunction = async () => await FindAsync<TEntity>(deleteRequest.Id);

            deleteResult.Should().Be(Unit.Value);
            findFunction.Should().Throw<EntityNotFoundException>();
        }

        public void ShouldThrowEntityNotFoundExceptionIfNoEntityFound<TResponse>(IRequest<TResponse> deleteRequest)
        {
            Func<Task> deleteFunction = async () => await SendAsync(deleteRequest);
            deleteFunction.Should().Throw<EntityNotFoundException>();
        }
    }
}
