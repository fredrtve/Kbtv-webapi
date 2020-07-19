using BjBygg.Application.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Application.Commands.MissionCommands.Update;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests
{
    using static AppTesting;

    public class UpdateMissionTests : AppTestBase
    {
        [Test]
        public void ShouldRequireValidMissionId()
        {
            var command = new UpdateMissionCommand
            {
                Id = 99,
                Address = "New Address"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateMission()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new UpdateMissionCommand
            {
                Id = 1,
                Address = "Updated Address",
                MissionType = new MissionTypeDto() { Id = 2 }, //Change type
                Employer = new EmployerDto() { Id = null }, //Set employer to null
            };

            await SendAsync(command);

            var entity = await FindAsync<Mission>(1);
            
            entity.Address.Should().Be(command.Address);
            entity.MissionTypeId.Should().Be(command.MissionType.Id);
            entity.EmployerId.Should().Be(null);
            entity.UpdatedBy.Should().NotBeNull();
            entity.UpdatedBy.Should().Be(user.UserName);
            entity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }

        [Test]
        public async Task ShouldUpdateMissionWithNewEmployerAndMissionType()
        {
            var command = new UpdateMissionCommand()
            {
                Id = 1,
                Address = "Test",
                MissionType = new MissionTypeDto() { Name = "ljkdngdggsdg" },
                Employer = new EmployerDto() { Name = "gsdagasdgsd" }
            };

            var response = await SendAsync(command);

            var missionType = (await GetAllAsync<MissionType>())
                .Find(x => x.Name == command.MissionType.Name);

            var employer = (await GetAllAsync<Employer>())
                .Find(x => x.Name == command.Employer.Name);

            var mission = await FindAsync<Mission>(response.Id);

            missionType.Should().NotBeNull();
            missionType.Name.Should().Be(command.MissionType.Name);

            employer.Should().NotBeNull();
            employer.Name.Should().Be(command.Employer.Name);

            mission.Should().NotBeNull();
            mission.MissionTypeId.Should().Be(missionType?.Id);
            mission.EmployerId.Should().Be(employer?.Id);
        }
    }
}
