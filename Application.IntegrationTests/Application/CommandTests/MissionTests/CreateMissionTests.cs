using BjBygg.Application.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core;
using BjBygg.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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

            await AddAsync(new Activity() { Id = "test", Name = "test" });
            await AddAsync(new Employer() { Id = "test", Name = "test" });

            var command = new CreateMissionCommand()
            {
                Id = "test",
                Address = "Test",
                MissionActivities = new System.Collections.Generic.List<MissionActivityDto>(){ 
                    new MissionActivityDto(){ Id = "test", ActivityId = "test" },
                    new MissionActivityDto(){ Id = "test2", Activity = new ActivityDto() { Id = "test2", Name = "newactivity" } },
                },
                EmployerId = "test",
            };

            await SendAsync(command);

            var dbEntity = await FindAsync<Mission>(command.Id);
            dbEntity.Should().NotBeNull();
            dbEntity.Address.Should().Be(command.Address);
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.EmployerId.Should().Be(command.EmployerId);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);

            var activities = (await GetAllAsync<MissionActivity>()).Where(x => x.MissionId == dbEntity.Id);
            activities.Should().HaveCount(3);
            activities.FirstOrDefault(x => x.Id == "test").Should().NotBeNull();
            activities.FirstOrDefault(x => x.Id == "test2").Should().NotBeNull();
            activities.FirstOrDefault(x => x.ActivityId == "default").Should().NotBeNull();
        }

        [Test]
        public async Task ShouldCreateMissionWithNewEmployer()
        {
            var command = new CreateMissionCommand()
            {
                Id = "test",
                Address = "Test",
                Employer = new EmployerDto() { Id = "newtest", Name = "gsdagasdgsd" }
            };

            await SendAsync(command);

            var employer = await FindAsync<Employer>(command.Employer.Id);

            var mission = await FindAsync<Mission>(command.Id);

            employer.Should().NotBeNull();
            employer.Name.Should().Be(command.Employer.Name);

            mission.Should().NotBeNull();
            mission.EmployerId.Should().Be(employer?.Id);
        }

    }
}
