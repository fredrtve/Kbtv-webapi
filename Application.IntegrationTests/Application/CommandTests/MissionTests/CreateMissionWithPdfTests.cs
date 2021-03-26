using BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
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
            var projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            var bytes = await File.ReadAllBytesAsync(projectDir + "\\data\\files\\pdf_report_test.pdf");

            var command = new CreateMissionWithPdfCommand()
            {
                Files = new DisposableList<BasicFileStream>{
                    { new BasicFileStream(new MemoryStream(bytes), "test.pdf") }
                }
            };

            await SendAsync(command);

            var mission = (await GetAllAsync<Mission>()).FirstOrDefault();

            var document = (await GetAllAsync<MissionDocument>()).FirstOrDefault();

            mission.Should().NotBeNull();
            document.Should().NotBeNull();
            mission.Address.Should().Be("Bekkasinveien 59, 2008 FJERDINGBY");
            mission.PhoneNumber.Should().Be("99522324");
            document.Name.Should().Be("Skaderapport");
            document.FileName.Should().NotBeNullOrEmpty();
            document.MissionId.Should().Be(mission.Id);
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
