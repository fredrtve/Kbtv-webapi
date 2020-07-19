using BjBygg.Application.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload;
using BjBygg.Application.Application.Commands.MissionCommands.Images.Upload;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionImageTests
{
    using static AppTesting;
    public class UploadMissionImagesTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new UploadMissionImageCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateMissionImagesWhenFilesUploaded()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var newMission = await SendAsync(new CreateMissionCommand { Address = "new mission" });

            await SendAsync(new UploadMissionImageCommand()
            {
                MissionId = newMission.Id,
                Files = new DisposableList<BasicFileStream>() {
                    new BasicFileStream(Encoding.UTF8.GetBytes("testimage1"), ".jpg"),
                    new BasicFileStream(Encoding.UTF8.GetBytes("testimage2"), ".jpg"),
                }
            });

            var dbEntities = (await GetAllAsync<MissionImage>()).Where(x => x.MissionId == newMission.Id);

            dbEntities.Should().NotBeNull();
            dbEntities.Should().HaveCount(2);

            dbEntities.First().FileURL.Should().BeOfType<Uri>();
            dbEntities.First().CreatedBy.Should().Be(user.UserName);
            dbEntities.First().UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }
    }
}
