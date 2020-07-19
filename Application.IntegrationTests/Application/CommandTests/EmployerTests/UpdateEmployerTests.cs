using BjBygg.Application.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.EmployerTests
{
    using static AppTesting;

    public class UpdateEmployerTests : AppTestBase
    {
        [Test]
        public void ShouldRequireValidEmployerId()
        {
            var command = new UpdateEmployerCommand
            {
                Id = 99,
                Name = "New Name"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateEmployer()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new UpdateEmployerCommand
            {
                Id = 1,
                Name = "Updated Name"
            };

            await SendAsync(command);

            var entity = await FindAsync<Employer>(1);

            entity.Name.Should().Be(command.Name);
            entity.UpdatedBy.Should().NotBeNull();
            entity.UpdatedBy.Should().Be(user.UserName);
            entity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
