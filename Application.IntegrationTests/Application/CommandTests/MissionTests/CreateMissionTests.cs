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
                Address = "Test",
                MissionType = new MissionTypeDto() { Id = 1 },
                Employer = new EmployerDto() { Id = 1 },
            };

            var entity = await SendAsync(command);

            var dbEntity = await FindAsync<Mission>(entity.Id);

            dbEntity.Should().NotBeNull();
            dbEntity.Address.Should().Be(command.Address);
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.MissionTypeId.Should().Be(command.MissionType.Id);
            dbEntity.EmployerId.Should().Be(command.Employer.Id);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }

        //[Test]
        //public async Task ShouldCreateMissionWithImageUri()
        //{
        //    var stream = new BasicFileStream(new MemoryStream(Encoding.UTF8.GetBytes("hvorfor fungerer ikke dette")), ".img");

        //    var command = new CreateMissionCommand() { 
        //        Address = "Test", 
        //        Image = stream
        //    };

        //    var entity = await SendAsync(command);

        //    var dbEntity = await FindAsync<Mission>(entity.Id);

        //    dbEntity.Should().NotBeNull();
        //    dbEntity.ImageURL.Should().BeOfType(typeof(Uri));
        //}

        [Test]
        public async Task ShouldCreateMissionWithNewEmployerAndMissionType()
        {
            var command = new CreateMissionCommand()
            {
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
