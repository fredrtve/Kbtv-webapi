using BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll;
using BjBygg.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.QueryTests.SyncTests
{
    using static Testing;

    //5 x all entities, created 2 yrs apart
    public class SyncAllTests : TestBase
    {
        [Test]
        public async Task ShouldReturnEntities()
        {
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
            var timestamp1 = DateTimeOffset.UtcNow.AddYears(-1).ToUnixTimeSeconds();

            var timestamp2 = DateTimeOffset.UtcNow.AddYears(-2).ToUnixTimeSeconds();

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
        public void ShouldThrowEntityNotFoundExceptionIfNoUserName()
        {
            Func<Task> f = async () =>
                await SendAsync(new SyncAllQuery() { InitialNumberOfMonths = 36 });

            f.Should().Throw<ValidationException>().WithMessage("No username provided");
        }

        [Test]
        public async Task ShouldReturnTimestampsFromNow()
        {
            var result = await SendAsync(new SyncAllQuery() { });
            var timestampFromNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            result.MissionSync.Timestamp.Should().BeApproximately(timestampFromNow, 10);
            result.MissionImageSync.Timestamp.Should().BeApproximately(timestampFromNow, 10);
            result.MissionNoteSync.Timestamp.Should().BeApproximately(timestampFromNow, 10);
            result.MissionDocumentSync.Timestamp.Should().BeApproximately(timestampFromNow, 10);
            result.MissionTypeSync.Timestamp.Should().BeApproximately(timestampFromNow, 10);
            result.EmployerSync.Timestamp.Should().BeApproximately(timestampFromNow, 10);
            result.DocumentTypeSync.Timestamp.Should().BeApproximately(timestampFromNow, 10);
            result.UserTimesheetSync.Timestamp.Should().BeApproximately(timestampFromNow, 10);
        }
    }
}