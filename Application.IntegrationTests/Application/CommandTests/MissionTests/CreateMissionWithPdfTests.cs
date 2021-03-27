using BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        public async Task ShouldCreateMissionFromPdfStyleOne()
        {
            await assertPdfsAsync(MissionPdfExtractExpectedResults.StrategyOneExpectedResults);
        }

        [Test]
        public async Task ShouldCreateMissionFromPdfStyleTwo()
        {
            await assertPdfsAsync(MissionPdfExtractExpectedResults.StrategyTwoExpectedResults);
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


        private async Task assertPdfsAsync(KeyValuePair<string, MissionPdfDto>[] keyValuePairs)
        {
            var projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            foreach (var keyValuePair in keyValuePairs)
            {
                var bytes = await File.ReadAllBytesAsync(projectDir + $"\\data\\files\\{keyValuePair.Key}");

                var command = new CreateMissionWithPdfCommand()
                {
                    Files = new DisposableList<BasicFileStream>{
                    { new BasicFileStream(new MemoryStream(bytes), keyValuePair.Key) }
                }
                };

                await SendAsync(command);

                var mission = (await GetAllAsync<Mission>()).FirstOrDefault();

                var document = (await GetAllAsync<MissionDocument>()).FirstOrDefault();

                mission.Should().NotBeNull();
                document.Should().NotBeNull();
                mission.Address.Should().Be(keyValuePair.Value.Address);
                mission.PhoneNumber.Should().Be(keyValuePair.Value.PhoneNumber);
                document.Name.Should().Be(keyValuePair.Value.DocumentName);
                document.FileName.Should().NotBeNullOrEmpty();
                document.MissionId.Should().Be(mission.Id);

                await RemoveAsync(document);
                await RemoveAsync(mission);
            }
        }
    }
}
