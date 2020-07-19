using BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests
{
    using static AppTesting;
    public class CreateMissionWithPdfTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateMissionWithPdfCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateMissionFromPdf()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
 
            var bytes = await File.ReadAllBytesAsync(projectDir + "\\data\\files\\pdf_report_test.pdf");

            var command = new CreateMissionWithPdfCommand()
            {
                Files = new DisposableList<BasicFileStream>{ 
                    { new BasicFileStream(new MemoryStream(bytes), ".pdf") } 
                }
            };

            var entity = await SendAsync(command);

            var dbEntity = await FindAsync<Mission>(entity.Id);

            var missionDocument = 
                (await GetAllAsync<MissionDocument>()).Find(x => x.MissionId == entity.Id);

            dbEntity.Should().NotBeNull();
            dbEntity.ImageURL.Should().BeOfType<Uri>();
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);

            missionDocument.Should().NotBeNull();
            missionDocument.FileURL.Should().BeOfType<Uri>();
        }

        [Test]
        public async Task ShouldThrowBadRequestExceptionIfInvalidFile()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            var bytes = await File.ReadAllBytesAsync(projectDir + "\\data\\files\\invalid_report_test.pdf");

            var command = new CreateMissionWithPdfCommand()
            {
                Files = new DisposableList<BasicFileStream>{
                    { new BasicFileStream(new MemoryStream(bytes), ".pdf") }
                }
            };

            FluentActions.Invoking(() =>
               SendAsync(command)).Should().Throw<BadRequestException>();
        }
    }
}
