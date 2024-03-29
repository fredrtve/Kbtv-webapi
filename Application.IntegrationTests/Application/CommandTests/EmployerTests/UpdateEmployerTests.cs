﻿using BjBygg.Application.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core;
using BjBygg.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
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
                Id = "notvalid",
                Name = "New Name"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateEmployer()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            await AddAsync(new Employer() { Id = "test", Name = "Test" });

            var command = new UpdateEmployerCommand
            {
                Id = "test",
                Name = "Updated Name"
            };

            await SendAsync(command);

            var entity = await FindAsync<Employer>("test");

            entity.Name.Should().Be(command.Name);
            entity.UpdatedBy.Should().NotBeNull();
            entity.UpdatedBy.Should().Be(user.UserName);
            entity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
