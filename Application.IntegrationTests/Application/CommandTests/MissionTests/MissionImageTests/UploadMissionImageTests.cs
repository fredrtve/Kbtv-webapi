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

            await SendAsync(new UploadMissionImageCommand()
            {
                MissionId = "test",
                Files = new DisposableList<BasicFileStream>() {
                    new BasicFileStream(Encoding.UTF8.GetBytes("testimage1"), "test.jpg")
                }
            });

            var dbEntities = (await GetAllAsync<MissionImage>()).Where(x => x.MissionId == "test");

            dbEntities.Should().NotBeNull();
            dbEntities.Should().HaveCount(1);

            dbEntities.First().Id.Should().Be("test");
            dbEntities.First().FileUri.Should().BeOfType<Uri>();
            dbEntities.First().CreatedBy.Should().Be(user.UserName);
            dbEntities.First().UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }
    }
}
