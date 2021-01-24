using BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll;
using BjBygg.Application.Common;
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

            result.Arrays.Missions.Entities.Should().HaveCount(1); //Not include raw add
            result.Arrays.MissionImages.Entities.Should().HaveCount(1); //Min date
            result.Arrays.MissionNotes.Entities.Should().HaveCount(1); //Min date
            result.Arrays.MissionDocuments.Entities.Should().HaveCount(1); //Min date
            result.Arrays.MissionTypes.Entities.Should().HaveCount(1);
            result.Arrays.Employers.Entities.Should().HaveCount(1);
            result.Arrays.UserTimesheets.Entities.Should().HaveCount(1); //User spesific & min date
        }

        [Test]
        public async Task ShouldReturnEntitiesModifiedAfterTimestamp()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var timestamp = DateTimeHelper.ConvertDateToEpoch(DateTimeHelper.Now().AddMinutes(-10));

            await AddSyncEntities();

            var result = await SendAsync(new SyncAllQuery()
            {
                InitialNumberOfMonths = 36,
                Timestamp = timestamp,
            });

            result.Arrays.Missions.Entities.Should().HaveCount(1);
            result.Arrays.MissionImages.Entities.Should().HaveCount(1);
            result.Arrays.MissionNotes.Entities.Should().HaveCount(1);
            result.Arrays.MissionDocuments.Entities.Should().HaveCount(1);
            result.Arrays.MissionTypes.Entities.Should().HaveCount(1);
            result.Arrays.Employers.Entities.Should().HaveCount(1);
            result.Arrays.UserTimesheets.Entities.Should().HaveCount(1);
        }

        [Test]
        public async Task ShouldReturnTimestampFromNow()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var result = await SendAsync(new SyncAllQuery() { });
            var timestampFromNow = DateTimeHelper.ConvertDateToEpoch(DateTimeHelper.Now());
            result.Timestamp.Should().BeApproximately(timestampFromNow * 1000, 1000);
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

            result.Arrays.Missions.Entities.Should().HaveCount(1);
            result.Arrays.MissionImages.Entities.Should().HaveCount(1);
            result.Arrays.MissionNotes.Entities.Should().HaveCount(1);
            result.Arrays.MissionDocuments.Entities.Should().HaveCount(1);
            result.Arrays.Employers.Entities.Should().HaveCount(1); //Only current employer should be returned
        }

        private static async Task AddSyncEntities()
        {
            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await AddAsync(new MissionImage() { Id = "test", MissionId = "test", FileName = "test.jpg" });
            await AddAsync(new MissionNote() { Id = "test", MissionId = "test", Content = "test" });
            await AddAsync(new MissionDocument() { Id = "test", MissionId = "test", FileName = "test.jpg" });
            await AddAsync(new MissionType() { Id = "test", Name = "test2" });
            await AddAsync(new Employer() { Id = "test", Name = "test2" });
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