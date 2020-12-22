using BjBygg.Application.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests
{
    using static AppTesting;

    public class CreateMissionTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateEmployerCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateMission()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            await AddAsync(new MissionType() { Id = "test", Name = "test" });
            await AddAsync(new Employer() { Id = "test", Name = "test" });

            var command = new CreateMissionCommand()
            {
                Id = "test",
                Address = "Test",
                MissionTypeId = "test",
                EmployerId = "test"
            };

            await SendAsync(command);

            var dbEntity = await FindAsync<Mission>(command.Id);

            dbEntity.Should().NotBeNull();
            dbEntity.Address.Should().Be(command.Address);
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.MissionTypeId.Should().Be(command.MissionTypeId);
            dbEntity.EmployerId.Should().Be(command.EmployerId);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }

        [Test]
        public async Task ShouldCreateMissionWithNewEmployerAndMissionType()
        {
            var command = new CreateMissionCommand()
            {
                Id = "test",
                Address = "Test",
                MissionType = new MissionTypeDto() { Id = "newtest", Name = "ljkdngdggsdg" },
                Employer = new EmployerDto() { Id = "newtest", Name = "gsdagasdgsd" }
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
