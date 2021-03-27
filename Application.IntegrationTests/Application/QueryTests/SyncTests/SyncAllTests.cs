using BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll;
using BjBygg.Application.Common;
using BjBygg.Core;
using BjBygg.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.QueryTests.SyncTests
{
    using static AppTesting;

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
            var initialTimestamp = DateTimeHelper.ConvertDateToEpoch(DateTime.Now.AddYears(-3)) * 1000;
            var result = await SendAsync(new SyncAllQuery() { Timestamp = initialTimestamp, InitialSync = true });

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

            await AddSyncEntities();

            /////** Test that children of mission in sync range is included despite itself not being in range **/
            //await AddSqlRaw("INSERT INTO MissionImages (Id, MissionId, FileName, Deleted, CreatedAt, UpdatedAt) " +
            //    $"VALUES ('test2','test', 'asdaa', 0, '{dateBeforeSync}', '{dateBeforeSync}')");
            await AddAsync(new Mission() { Id = "test2", Address = "test" });
            /** Test that children of mission out of sync range is not included despite itself being in range **/
            await AddAsync(new MissionDocument() { Id = "test3", MissionId = "test2", Name = "test", FileName = "test.jpg" });
            await AddAsync(new MissionImage() { Id = "test3", MissionId = "test2", FileName = "test.jpg" });
            await AddAsync(new MissionNote() { Id = "test3", MissionId = "test2", Content = "test.jdsadapg" });

            var timestamp = DateTimeHelper.ConvertDateToEpoch(DateTimeHelper.Now().AddMinutes(-10)) * 1000;

            var dateBeforeSync = DateTime.Now.AddYears(-4).ToString("yyyy-MM-dd HH:mm:ss");

            await AddSqlRaw($"UPDATE Missions SET UpdatedAt = '{dateBeforeSync}' WHERE id='test2'");

            var result = await SendAsync(new SyncAllQuery()
            {
                Timestamp = timestamp,
            });

            result.Arrays.Missions.Entities.Should().HaveCount(1);
            result.Arrays.MissionImages.Entities.Should().HaveCount(2);
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
            await AddAsync(new MissionDocument() { Id = "test", MissionId = "test2", Name = "test", FileName = "test.jpg" });
            await AddAsync(new MissionType() { Id = "test", Name = "test2" });


            var result = await SendAsync(new SyncAllQuery() { });

            result.Arrays.Missions.Entities.Should().HaveCount(1);
            result.Arrays.MissionImages.Entities.Should().HaveCount(1);
            result.Arrays.MissionNotes.Entities.Should().HaveCount(1);
            result.Arrays.MissionDocuments.Entities.Should().HaveCount(0);
            result.Arrays.Employers.Entities.Should().HaveCount(1); //Only current employer should be returned
        }

        private static async Task AddSyncEntities()
        {
            await AddAsync(new Mission() { Id = "test", Address = "test" });
            await AddAsync(new MissionImage() { Id = "test", MissionId = "test", FileName = "test.jpg" });
            await AddAsync(new MissionNote() { Id = "test", MissionId = "test", Content = "test" });
            await AddAsync(new MissionDocument() { Id = "test", MissionId = "test", Name = "test", FileName = "test.jpg" });
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