using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static void Seed(AppDbContext context)
        {
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();
                if (!context.Employers.Any())
                {
                    context.Database.OpenConnection();
                    context.Employers.AddRange(
                        GetPreconfiguredEmployers());

                    context.SaveChanges();
                    context.Database.CloseConnection();
                }
                
                if (!context.MissionTypes.Any())
                {
                    context.Database.OpenConnection();
                    context.MissionTypes.AddRange(
                        GetPreconfiguredMissionTypes());

                    context.SaveChanges();
                    context.Database.CloseConnection();
                }
                if (!context.ReportTypes.Any())
                {
                    context.Database.OpenConnection();
                    context.ReportTypes.AddRange(
                        GetPreconfiguredReportTypes());

                    context.SaveChanges();
                    context.Database.CloseConnection();
                }
                if (!context.Missions.Any())
                {
                    context.Database.OpenConnection();
                    context.Missions.AddRange(
                        GetPreconfiguredMissions());

                    context.SaveChanges();
                    context.Database.CloseConnection();
                }
                if (!context.MissionReports.Any())
                {
                    context.Database.OpenConnection();
                    context.MissionReports.AddRange(
                        GetPreconfiguredMissionReports());

                    context.SaveChanges();
                    context.Database.CloseConnection();
                }
                if (!context.MissionNotes.Any())
                {
                    context.Database.OpenConnection();
                    context.MissionNotes.AddRange(
                        GetPreconfiguredMissionNotes());

                    context.SaveChanges();
                    context.Database.CloseConnection();
                }
                if (!context.Timesheets.Any())
                {
                    context.Database.OpenConnection();
                    context.Timesheets.AddRange(
                        GetPreconfiguredTimesheets());

                    context.SaveChanges();
                    context.Database.CloseConnection();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static IEnumerable<Employer> GetPreconfiguredEmployers()
        {
            return new List<Employer>()
            {
                new Employer() { Id = 1, Name = "NSU" },
                new Employer() { Id = 2, Name = "BSU" },
            };
        }
        static IEnumerable<MissionType> GetPreconfiguredMissionTypes()
        {
            return new List<MissionType>()
            {
                new MissionType() { Id = 1, Name = "Riving" },
                new MissionType() { Id = 2, Name = "Oppbygging" }
            };
        }

        static IEnumerable<ReportType> GetPreconfiguredReportTypes()
        {
            return new List<ReportType>()
            {
                new ReportType() { Id = 1, Name = "Skaderapport" },
                new ReportType() { Id = 2, Name = "Tørkerapport" }
            };
        }

        static IEnumerable<Mission> GetPreconfiguredMissions()
        {
            var missions = new List<Mission>();
            for (var i = 1; i <= 15; i++)
            {
                missions.Add(new Mission()
                {
                    Id = i,
                    Name = $"Oppdrag {i}",
                    PhoneNumber = "92278483",
                    Description = "Dette er en beskrivelse",
                    Address = $"Furuberget {i}, 1940 Bjørkelangen",
                    EmployerId = 1,
                    MissionTypeId = 1,
                });
            }

            return missions;
        }
        static IEnumerable<MissionReport> GetPreconfiguredMissionReports()
        {
            return new List<MissionReport>()
            {
                new MissionReport() { Id = 1, FileURL = new Uri("https://kbtv.blob.core.windows.net/images/28e89dfa-8d9b-422f-81fd-ee1f7aafbbe7.jpg"), MissionId = 1, ReportTypeId = 1 },
                new MissionReport() { Id = 2, FileURL = new Uri("https://kbtv.blob.core.windows.net/images/28e89dfa-8d9b-422f-81fd-ee1f7aafbbe7.jpg"), MissionId = 1, ReportTypeId = 2 }
            };
        }

        static IEnumerable<MissionNote> GetPreconfiguredMissionNotes()
        {
            var missionNotes = new List<MissionNote>();
            var idCounter = 1;

            for (var missionId = 1; missionId <= 15; missionId++)
            {
                for (var p = 1; p <= 3; p++)
                {
                    missionNotes.Add(new MissionNote()
                    {
                        Id = idCounter,
                        MissionId = missionId,
                        Content = "Dette er ett veldig interessant notat. Veldig interessant. Det er også veldig viktig.",
                        Title =  "Dette er ett notat",
                        Pinned = false
                    });
                    idCounter++;

                    missionNotes.Add(new MissionNote()
                    {
                        Id = idCounter,
                        MissionId = missionId,
                        Content = "Dette er ett veldig interessant og viktig notat. Veldig viktig og interessant. Det er også veldig viktig.",
                        Title = "Dette er ett viktig notat",
                        Pinned = true
                    });

                    idCounter++;
                }
            }



            return missionNotes;
        }

        static IEnumerable<Timesheet> GetPreconfiguredTimesheets()
        {
            var timesheets = new List<Timesheet>();
            var idCounter = 1;
            var dayCounter = 0;
            var rnd = new Random();
            var today = DateTime.Now.Date.AddHours(6);

            for (var n = 1; n <= 100; n++)
            {        
                for (var missionId = 1; missionId <= 15; missionId++)
                {
                    var startDate = today.AddDays(-dayCounter);
                    var endDate = startDate.AddHours(rnd.Next(4, 10));
                    timesheets.Add(new Timesheet()
                    {
                        Id = idCounter,
                        MissionId = missionId,
                        StartTime = startDate,
                        EndTime = endDate,
                        TotalHours = (endDate - startDate).TotalHours,
                        Comment = "Dette er en kommentar til en time for leder for ett kult oppdrag",
                        UserName = "leder",
                        Status = n % 2 == 0 ? TimesheetStatus.Open : TimesheetStatus.Confirmed
                    });
                    idCounter++;

                    timesheets.Add(new Timesheet()
                    {
                        Id = idCounter,
                        MissionId = missionId,
                        StartTime = startDate,
                        EndTime = endDate,
                        TotalHours = (endDate - startDate).TotalHours,
                        Comment = "Dette er en kommentar til en time for ansatt",
                        UserName = "ansatt",
                        Status = n % 2 == 0 ? TimesheetStatus.Open : TimesheetStatus.Confirmed
                    });
                    idCounter++;

                    timesheets.Add(new Timesheet()
                    {
                        Id = idCounter,
                        MissionId = missionId,
                        StartTime = startDate,
                        EndTime = endDate,
                        TotalHours = (endDate - startDate).TotalHours,
                        Comment = "Dette er en kommentar til en time for mellomleder for ett alle tiders oppdrag",
                        UserName = "mellomleder",
                        Status = n % 2 == 0 ? TimesheetStatus.Open : TimesheetStatus.Confirmed
                    });
                    idCounter++;

                    dayCounter++;
                }
       
            }



            return timesheets;
        }
    }
}
