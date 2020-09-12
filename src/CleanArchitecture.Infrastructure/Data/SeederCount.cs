using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Infrastructure.Data
{
    public class SeederCount
    {
        public SeederCount(
            int missions = 1500,
            int missionTypes = 30,
            int missionNotes = 3500,
            int missionDocuments = 3000,
            int missionImages = 3500,
            int employers = 30,
            int timesheets = 7000,
            int documentTypes = 30)
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
                [typeof(DocumentType)] = documentTypes,
            };
        }

        public Dictionary<Type, int> SeedCounts { get; private set; }
    }
}