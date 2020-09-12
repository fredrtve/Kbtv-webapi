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
            var preCreationMissions = await GetAllAsync<Mission>();
            var command = new CreateMissionWithPdfCommand()
            {
                Files = new DisposableList<BasicFileStream>{ 
                    { new BasicFileStream(new MemoryStream(bytes), "test.pdf") } 
                }
            };

            await SendAsync(command);

            var pastCreationMissions = await GetAllAsync<Mission>();
            var newMissionsCount = pastCreationMissions.Count - preCreationMissions.Count;
            //(await GetAllAsync<MissionDocument>()).Find(x => x.MissionId == response.Mission.Id);

            newMissionsCount.Should().Be(1);
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
