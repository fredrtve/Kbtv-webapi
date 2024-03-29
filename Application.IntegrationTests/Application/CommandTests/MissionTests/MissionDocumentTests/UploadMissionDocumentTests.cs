﻿using BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.SharedKernel;
using FluentAssertions;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionDocumentTests
{
    using static AppTesting;
    public class UploadMissionDocumentTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new UploadMissionDocumentCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateMissionDocumentWhenFileUploaded()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            await AddAsync(new Mission() { Id = "test", Address = "test" });

            var command = new UploadMissionDocumentCommand
            {
                Id = "test",
                MissionId = "test",
                Name = "test",
                File = new MemoryStream(Encoding.UTF8.GetBytes("testdocument")),
                FileExtension = ".pdf"
            };

            await SendAsync(command);

            var dbEntity = await FindAsync<MissionDocument>(command.Id);

            dbEntity.Should().NotBeNull();
            dbEntity.MissionId.Should().Be(command.MissionId);
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }

    }
}
