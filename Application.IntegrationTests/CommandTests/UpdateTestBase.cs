using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.SharedKernel;
using FluentAssertions;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CommandTests
{
    using static Testing;

    public abstract class UpdateTestBase<TEntity> : TestBase where TEntity : BaseEntity
    {

        public void ShouldThrowEntityNotFoundExceptionIfNoEntityFound<TResponse>(IRequest<TResponse> updateRequest)
        {
            Func<Task> updateFunction = async () => await SendAsync(updateRequest);
            updateFunction.Should().Throw<EntityNotFoundException>();
        }

    }
}
