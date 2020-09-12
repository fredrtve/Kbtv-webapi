using BjBygg.Application.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Application.Commands.MissionCommands.Update;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Text;
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
                Id = "notvalid",
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
                Id = "test",
                Address = "Updated Address",
                PhoneNumber = "92278483",
                Description = "asdasd",
                MissionType = new MissionTypeDto() { Id = "test" }, //Change type
                Employer = new EmployerDto() { Id = null }, //Set employer to null
                //Image = new BasicFileStream(Encoding.UTF8.GetBytes("testimg"), ".img")
            };

            await SendAsync(command);

            var dbMission = await FindAsync<Mission>(command.Id);

            dbMission.Address.Should().Be(command.Address);
            dbMission.PhoneNumber.Should().Be(command.PhoneNumber);
            dbMission.MissionTypeId.Should().Be(command.MissionType.Id);
            dbMission.EmployerId.Should().Be(null);
            //dbMission.FileUri.Should().BeOfType(typeof(Uri));
            dbMission.UpdatedBy.Should().NotBeNull();
            dbMission.UpdatedBy.Should().Be(user.UserName);
            dbMission.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }

        [Test]
        public async Task ShouldUpdateMissionWithNewEmployerAndMissionType()
        {
            var command = new UpdateMissionCommand()
            {
                Id = "test",
                Address = "Test",
                MissionType = new MissionTypeDto() { Id = "newid", Name = "ljkdngdggsdg" },
                Employer = new EmployerDto() { Id = "newid", Name = "gsdagasdgsd" }
            };

            await SendAsync(command);

            var missionType = await FindAsync<MissionType>(command.MissionType.Id);

            var employer = await FindAsync<Employer>(command.Employer.Id);

            var mission = await FindAsync<Mission>(command.Id);

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
