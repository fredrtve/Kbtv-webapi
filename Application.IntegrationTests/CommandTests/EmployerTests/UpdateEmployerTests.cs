using BjBygg.Application.Application.Commands.EmployerCommands.Update;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.CommandTests.EmployerTests
{
    using static Testing;

    public class UpdateEmployerTests : UpdateTestBase<Employer>
    {
        [Test]
        public async Task ShouldUpdateEmployer()
        {
            var command = new UpdateEmployerCommand() { Id = 3, Name = "updated", Address = "Test123" };
            await SendAsync(command);
            var updatedEntity = await FindAsync<Employer>(command.Id);
            updatedEntity.Name.Should().Be(command.Name);
            updatedEntity.Address.Should().Be(command.Address);
            updatedEntity.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, 50);
        }

        [Test]
        public void ShouldThrowEntityNotFoundExceptionIfNoEmployerFound()
        {
            base.ShouldThrowEntityNotFoundExceptionIfNoEntityFound(new UpdateEmployerCommand() { Id = 70 });
        }
    }
}
