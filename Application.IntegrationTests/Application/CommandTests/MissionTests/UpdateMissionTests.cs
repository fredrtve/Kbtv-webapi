using BjBygg.Application.Application.Commands.MissionCommands.Update;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core;
using BjBygg.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
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

            await AddAsync(new Activity() { Id = "test", Name = "test" });
            await AddAsync(new Employer() { Id = "test", Name = "test" });
            await AddAsync(new Mission() { Id = "test", Address = "test", EmployerId = "test" });

            var command = new UpdateMissionCommand
            {
                Id = "test",
                Address = "Updated Address",
                PhoneNumber = "92278483",
                Description = "asdasd",
                MissionActivities = new System.Collections.Generic.List<MissionActivityDto>(){
                    new MissionActivityDto(){ Id = "test", ActivityId = "test" },
                    new MissionActivityDto(){ Id = "test2", Activity = new ActivityDto() { Id = "test2", Name = "newactivity" } },
                },
                EmployerId = null, //Set employer to null
            };

            await SendAsync(command);

            var dbMission = await FindAsync<Mission>(command.Id);

            dbMission.Address.Should().Be(command.Address);
            dbMission.PhoneNumber.Should().Be(command.PhoneNumber);
            dbMission.EmployerId.Should().Be(null);
            dbMission.UpdatedBy.Should().NotBeNull();
            dbMission.UpdatedBy.Should().Be(user.UserName);
            dbMission.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);

            var activities = (await GetAllAsync<MissionActivity>()).Where(x => x.MissionId == dbMission.Id);
            activities.Should().HaveCount(2);
            activities.FirstOrDefault(x => x.Id == "test").Should().NotBeNull();
            activities.FirstOrDefault(x => x.Id == "test2").Should().NotBeNull();
        }

        [Test]
        public async Task ShouldUpdateMissionWithNewEmployer()
        {
            await AddAsync(new Mission() { Id = "test", Address = "test" });

            var command = new UpdateMissionCommand()
            {
                Id = "test",
                Address = "Test",
                Employer = new EmployerDto() { Id = "newid", Name = "gsdagasdgsd" }
            };

            await SendAsync(command);


            var employer = await FindAsync<Employer>(command.Employer.Id);

            var mission = await FindAsync<Mission>(command.Id);

            employer.Should().NotBeNull();
            employer.Name.Should().Be(command.Employer.Name);

            mission.Should().NotBeNull();
            mission.EmployerId.Should().Be(employer?.Id);
        }

        [Test]
        public async Task ShouldNotRemoveExistingMissionActivities()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            await AddAsync(new Activity() { Id = "test", Name = "test" });
            await AddAsync(new Employer() { Id = "test", Name = "test" });
            await AddAsync(new Mission() { Id = "test", Address = "test", EmployerId = "test" });
            await AddAsync(new MissionActivity() { Id = "test", MissionId = "test", ActivityId = "test" });

            var command = new UpdateMissionCommand
            {
                Id = "test",
                Address = "Updated Address",
                PhoneNumber = "92278483",
                Description = "asdasd",
                MissionActivities = new System.Collections.Generic.List<MissionActivityDto>(){
                    new MissionActivityDto(){ Id = "test2", Activity = new ActivityDto() { Id = "test2", Name = "newactivity" } },
                },
                EmployerId = null,
            };

            await SendAsync(command);

            var dbMission = await FindAsync<Mission>(command.Id);

            var activities = (await GetAllAsync<MissionActivity>()).Where(x => x.MissionId == dbMission.Id);
            activities.Should().HaveCount(2);
            activities.FirstOrDefault(x => x.Id == "test").Should().NotBeNull();
        }
    }
}
