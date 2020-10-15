using BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.QueryTests.SyncTests
{
    using static AppTesting;

    //5 x all entities, created 2 yrs apart
    public class SyncAllTests : AppTestBase
    {
        [Test]

        public async Task ShouldReturnEntities()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var dateBeforeSync = DateTime.Now.AddYears(-4).ToString("yyyy-MM-dd HH:mm:ss");
            await AddSqlRaw("INSERT INTO Missions (Id, Address, Deleted, CreatedAt, UpdatedAt) " +
                $"VALUES ('testRaw','TestAddress', 0, '{dateBeforeSync}', '{dateBeforeSync}')");

            await AddSyncEntities();

            var result = await SendAsync(new SyncAllQuery() { InitialNumberOfMonths = 36 });

            result.MissionSync.Entities.Should().HaveCount(1); //Not include raw add
            result.MissionImageSync.Entities.Should().HaveCount(1); //Min date
            result.MissionNoteSync.Entities.Should().HaveCount(1); //Min date
            result.MissionDocumentSync.Entities.Should().HaveCount(1); //Min date
            result.MissionTypeSync.Entities.Should().HaveCount(1);
            result.EmployerSync.Entities.Should().HaveCount(1);
            result.DocumentTypeSync.Entities.Should().HaveCount(1);
            result.UserTimesheetSync.Entities.Should().HaveCount(1); //User spesific & min date
        }

        [Test]
        public async Task ShouldReturnEntitiesModifiedAfterTimestamp()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var timestamp1 = DateTimeHelper.ConvertDateToEpoch(DateTimeHelper.Now().AddMinutes(-10));

            var timestamp2 = DateTimeHelper.ConvertDateToEpoch(DateTimeHelper.Now().AddMinutes(10));

            await AddSyncEntities();

            var result = await SendAsync(new SyncAllQuery()
            {
                InitialNumberOfMonths = 36,
                MissionTimestamp = timestamp1,
                MissionImageTimestamp = timestamp1,
                MissionNoteTimestamp = timestamp1,
                MissionDocumentTimestamp = timestamp1,
                MissionTypeTimestamp = timestamp2,
                EmployerTimestamp = timestamp2,
                DocumentTypeTimestamp = timestamp2,
                UserTimesheetTimestamp = timestamp2,
            });

            result.MissionSync.Entities.Should().HaveCount(1);
            result.MissionImageSync.Entities.Should().HaveCount(1);
            result.MissionNoteSync.Entities.Should().HaveCount(1);
            result.MissionDocumentSync.Entities.Should().HaveCount(1);
            result.MissionTypeSync.Entities.Should().HaveCount(0);
            result.EmployerSync.Entities.Should().HaveCount(0);
            result.DocumentTypeSync.Entities.Should().HaveCount(0);
            result.UserTimesheetSync.Entities.Should().HaveCount(0);
        }

        [Test]
        public async Task ShouldReturnTimestampsFromNow()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var result = await SendAsync(new SyncAllQuery() { });
            var timestampFromNow = DateTimeHelper.ConvertDateToEpoch(DateTimeHelper.Now());
            result.MissionSync.Timestamp.Should().BeApproximately(timestampFromNow, 1000);
            result.MissionImageSync.Timestamp.Should().BeApproximately(timestampFromNow, 1000);
            result.MissionNoteSync.Timestamp.Should().BeApproximately(timestampFromNow, 1000);
            result.MissionDocumentSync.Timestamp.Should().BeApproximately(timestampFromNow, 1000);
            result.MissionTypeSync.Timestamp.Should().BeApproximately(timestampFromNow, 1000);
            result.EmployerSync.Timestamp.Should().BeApproximately(timestampFromNow, 1000);
            result.DocumentTypeSync.Timestamp.Should().BeApproximately(timestampFromNow, 1000);
            result.UserTimesheetSync.Timestamp.Should().BeApproximately(timestampFromNow, 1000);
        }

        [Test]
        public async Task ShouldReturnEmployerSpesificResourcesIfUserIsEmployer()
        {
            await AddAsync(new Employer() { Id = "test", Name = "test" });
           
            await RunAsDefaultUserAsync(Roles.Employer, "test");

            await AddAsync(new Employer() { Id = "test2", Name = "test2" });
            await AddAsync(new Mission() { Id = "test2", Address = "test", EmployerId = "test2" });

            await AddAsync(new Mission() { Id = "test", Address = "test", EmployerId = "test" });
            await AddAsync(new MissionImage() { Id = "test", MissionId = "test", FileName = "test.jpg" });
            await AddAsync(new MissionNote() { Id = "test", MissionId = "test", Content = "test" });
            await AddAsync(new MissionDocument() { Id = "test", MissionId = "test", FileName = "test.jpg" });
            await AddAsync(new MissionType() { Id = "test", Name = "test2" });
           

            var result = await SendAsync(new SyncAllQuery() { });

            result.MissionSync.Entities.Should().HaveCount(1); 
            result.MissionImageSync.Entities.Should().HaveCount(1); 
            result.MissionNoteSync.Entities.Should().HaveCount(1); 
            result.MissionDocumentSync.Entities.Should().HaveCount(1); 
            result.EmployerSync.Entities.Should().HaveCount(1); //Only current employer should be returned
        }

        private static async Task AddSyncEntities()
        {
            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await AddAsync(new MissionImage() { Id = "test", MissionId = "test", FileName = "test.jpg" });
            await AddAsync(new MissionNote() { Id = "test", MissionId = "test", Content = "test" });
            await AddAsync(new MissionDocument() { Id = "test", MissionId = "test", FileName = "test.jpg" });
            await AddAsync(new MissionType() { Id = "test", Name = "test2" });
            await AddAsync(new Employer() { Id = "test", Name = "test2" });
            await AddAsync(new DocumentType() { Id = "test", Name = "test2" });
            await AddAsync(new Timesheet()
            {
                Id = "test",
                MissionId = "test",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(4),
                Comment = "asdfd",
                UserName = Roles.Leader
            });
        }
    }
}