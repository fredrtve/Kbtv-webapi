using BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
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
            var result = await SendAsync(new SyncAllQuery() { InitialNumberOfMonths = 36 });

            result.MissionSync.Entities.Should().HaveCount(2); //Min date
            result.MissionImageSync.Entities.Should().HaveCount(2); //Min date
            result.MissionNoteSync.Entities.Should().HaveCount(2); //Min date
            result.MissionDocumentSync.Entities.Should().HaveCount(2); //Min date
            result.MissionTypeSync.Entities.Should().HaveCount(5);
            result.EmployerSync.Entities.Should().HaveCount(5);
            result.DocumentTypeSync.Entities.Should().HaveCount(5);
            result.UserTimesheetSync.Entities.Should().HaveCount(4); //User spesific & min date
        }

        [Test]
        public async Task ShouldReturnEntitiesModifiedAfterTimestamp()
        {
            await RunAsDefaultUserAsync(Roles.Leader);

            var timestamp1 = DateTimeHelper.ConvertDateToEpoch(DateTimeHelper.Now().AddYears(-1).AddMinutes(-1));

            var timestamp2 = DateTimeHelper.ConvertDateToEpoch(DateTimeHelper.Now().AddYears(-2).AddMinutes(-1));

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
            result.MissionTypeSync.Entities.Should().HaveCount(2);
            result.EmployerSync.Entities.Should().HaveCount(2);
            result.DocumentTypeSync.Entities.Should().HaveCount(2);
            result.UserTimesheetSync.Entities.Should().HaveCount(4);
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
            await RunAsDefaultUserAsync(Roles.Employer);

            var result = await SendAsync(new SyncAllQuery() { });

            result.MissionSync.Entities.Should().HaveCount(1); 
            result.MissionImageSync.Entities.Should().HaveCount(1); 
            result.MissionNoteSync.Entities.Should().HaveCount(1); 
            result.MissionDocumentSync.Entities.Should().HaveCount(1); 
            result.EmployerSync.Entities.Should().HaveCount(1); //Only current employer should be returned
        }
    }
}