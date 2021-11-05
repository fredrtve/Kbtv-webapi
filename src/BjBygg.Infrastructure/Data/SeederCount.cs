using BjBygg.Core.Entities;
using System;
using System.Collections.Generic;

namespace BjBygg.Infrastructure.Data
{
    public class SeederCount
    {
        public SeederCount(
            int missions = 200,
            int missionNotes = 500,
            int missionDocuments = 500,
            int missionImages = 500,
            int employers = 30,
            int activities = 30,
            int timesheets = 500)
        {
            SeedCounts = new Dictionary<Type, int>()
            {
                [typeof(Mission)] = missions,
                [typeof(MissionNote)] = missionNotes,
                [typeof(MissionDocument)] = missionDocuments,
                [typeof(MissionImage)] = missionImages,
                [typeof(Employer)] = employers,
                [typeof(Timesheet)] = timesheets,
                [typeof(Activity)] = activities,
            };
        }

        public Dictionary<Type, int> SeedCounts { get; private set; }
    }
}