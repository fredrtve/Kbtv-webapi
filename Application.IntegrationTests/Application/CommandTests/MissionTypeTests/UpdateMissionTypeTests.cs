using BjBygg.Application.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Application.Commands.MissionTypeCommands.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTypeTests
{
    using static AppTesting;

    public class UpdateMissionTypeTests : AppTestBase
    {
        [Test]
        public void ShouldRequireValidMissionTypeId()
        {
            var command = new UpdateMissionTypeCommand
            {
                Id = "notvalid",
                Name = "New Name"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateMissionType()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            await AddAsync(new MissionType() { Id = "test", Name = "test" });

            var command = new UpdateMissionTypeCommand
            {
                Id = "test",
                Name = "Updated Name"
            };

            await SendAsync(command);

            var entity = await FindAsync<MissionType>("test");

            entity.Name.Should().Be(command.Name);
            entity.UpdatedBy.Should().NotBeNull();
            entity.UpdatedBy.Should().Be(user.UserName);
            entity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}
