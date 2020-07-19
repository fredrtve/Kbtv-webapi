using BjBygg.Application.Application.Queries.TimesheetQueries;
using BjBygg.Application.Common;
using CleanArchitecture.Core;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.QueryTests
{
    using static AppTesting;
    public class TimesheetQueryTests : AppTestBase
    {
        [Test]
        public async Task ShouldReturnAllTimesheetIfEmptyQuery()
        {
            var timesheets = await SendAsync(new TimesheetQuery());
            timesheets.Should().HaveCount(5);
        }

        [Test]
        public async Task ShouldReturnAllTimesheetInGivenDateRange()
        {
            var now = DateTimeHelper.Now();

            var query = new TimesheetQuery()
            {
                StartDate = DateTimeHelper.ConvertDateToEpoch(now.AddDays(-2).Date),
                EndDate = DateTimeHelper.ConvertDateToEpoch(now.Date),
            };

            var timesheets = await SendAsync(query);
            timesheets.Should().HaveCount(3);
        }

        [Test]
        public async Task ShouldReturnAllTimesheetForGivenUser()
        {
            var query = new TimesheetQuery()
            {
                UserName = Roles.Management
            };

            var timesheets = await SendAsync(query);
            timesheets.Should().HaveCount(1);
        }

        [Test]
        public async Task ShouldReturnAllTimesheetForGivenMission()
        {
            var query = new TimesheetQuery()
            {
                MissionId = 2
            };

            var timesheets = await SendAsync(query);
            timesheets.Should().HaveCount(1);
        }
    }
}
