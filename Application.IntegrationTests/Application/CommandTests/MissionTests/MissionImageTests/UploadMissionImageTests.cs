using BjBygg.Application.Application.Commands.MissionCommands.Images.Upload;
using BjBygg.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;

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

        //[Test]
        //public async Task ShouldCreateMissionImagesWhenFilesUploaded()
        //{
        //    var user = await RunAsDefaultUserAsync(Roles.Leader);
        //    var fileName = "test.jpg";

        //    await SendAsync(new UploadMissionImageCommand()
        //    {
        //        MissionId = "test",
        //        Files = new DisposableList<BasicFileStream>() {
        //            new BasicFileStream(Encoding.UTF8.GetBytes("testimage1"), fileName)
        //        }
        //    });

        //    var dbEntities = (await GetAllAsync<MissionImage>()).Where(x => x.MissionId == "test");

        //    dbEntities.Should().NotBeNull();
        //    dbEntities.Should().HaveCount(1);

        //    dbEntities.First().Id.Should().Be("test");
        //    dbEntities.First().FileName.Should().Be(fileName); 
        //    dbEntities.First().CreatedBy.Should().Be(user.UserName);
        //    dbEntities.First().UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        //}
    }
}
