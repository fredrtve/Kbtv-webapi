using BjBygg.Application.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
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

            var command = new CreateMissionCommand() {
                Id = "test",
                Address = "Test",
                MissionType = new MissionTypeDto() { Id = "test" },
                Employer = new EmployerDto() { Id = "test" },
                Image = new BasicFileStream(Encoding.UTF8.GetBytes("testimg"), ".img")
            };

            await SendAsync(command);

            var dbEntity = await FindAsync<Mission>(command.Id);

            dbEntity.Should().NotBeNull();
            dbEntity.Address.Should().Be(command.Address);
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.MissionTypeId.Should().Be(command.MissionType.Id);
            dbEntity.FileUri.Should().BeOfType(typeof(Uri));
            dbEntity.EmployerId.Should().Be(command.Employer.Id);
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
