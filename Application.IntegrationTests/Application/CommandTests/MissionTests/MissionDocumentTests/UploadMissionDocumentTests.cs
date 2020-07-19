using BjBygg.Application.Application.Commands.MissionCommands.Documents.Upload;
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

            var command = new UploadMissionDocumentCommand{ 
                MissionId = 1, 
                DocumentType = new DocumentTypeDto { Id = 1 },
                File = new BasicFileStream(Encoding.UTF8.GetBytes("testdocument"), ".pdf")
            };

            var entity = await SendAsync(command);

            var dbEntity = await FindAsync<MissionDocument>(entity.Id);

            dbEntity.Should().NotBeNull();
            dbEntity.MissionId.Should().Be(command.MissionId);
            dbEntity.DocumentTypeId.Should().Be(command.DocumentType.Id);
            dbEntity.FileURL.Should().BeOfType<Uri>();
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }

        [Test]
        public async Task ShouldCreateMissionDocumentWhenFileUploadedWithNewDocumentType()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new UploadMissionDocumentCommand
            {
                MissionId = 1,
                DocumentType = new DocumentTypeDto { Name = "new type" },
                File = new BasicFileStream(Encoding.UTF8.GetBytes("testdocument"), ".pdf")
            };

            var entity = await SendAsync(command);

            var dbEntity = await FindAsync<MissionDocument>(entity.Id);
            var dbDocumentType = (await GetAllAsync<DocumentType>()).Find(x => x.Name == command.DocumentType.Name);

            dbDocumentType.Should().NotBeNull();
            dbEntity.Should().NotBeNull();
            dbEntity.DocumentTypeId.Should().Be(dbDocumentType.Id);
        }
    }
}
