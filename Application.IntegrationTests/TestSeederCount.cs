using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;

namespace Application.IntegrationTests
{
    public class TestSeederCount
    {
        public TestSeederCount(
            int missions = 5,
            int missionTypes = 5,
            int missionNotes = 5,
            int missionDocuments = 5,
            int missionImages = 5,
            int employers = 5,
            int timesheets = 5,
            int documentTypes = 5)
        {
            SeedCounts = new Dictionary<Type, int>()
            {
                [typeof(Mission)] = missions,
                [typeof(MissionType)] = missionTypes,
                [typeof(MissionNote)] = missionNotes,
                [typeof(MissionDocument)] = missionDocuments,
                [typeof(MissionImage)] = missionImages,
                [typeof(Employer)] = employers,
                [typeof(Timesheet)] = timesheets,
            };
        }
        public Dictionary<Type, int> SeedCounts { get; private set; }
    }
}
