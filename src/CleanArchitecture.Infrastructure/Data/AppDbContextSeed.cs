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
        readonly static Random random = new Random();

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
                new Employer() { Id = 1, Name = "NSU", Email = "NSU@test.no" },
                new Employer() { Id = 2, Name = "BSU", Email = "BSU@test.no" },
                new Employer() { Id = 3, Name = "RSU", Email = "RSU@test.no" },
                new Employer() { Id = 4, Name = "TSU", Email = "TSU@test.no" },
                new Employer() { Id = 5, Name = "Privat", Email = "Privat@test.no" },
                new Employer() { Id = 6, Name = "Jens Bjarne AS", Email = "jens@test.no" },
            };
        }
        static IEnumerable<MissionType> GetPreconfiguredMissionTypes()
        {
            return new List<MissionType>()
            {
                new MissionType() { Id = 1, Name = "Riving" },
                new MissionType() { Id = 2, Name = "Oppbygging" },
            };
        }

        static IEnumerable<ReportType> GetPreconfiguredReportTypes()
        {
            return new List<ReportType>()
            {
                new ReportType() { Id = 1, Name = "Skaderapport" },
                new ReportType() { Id = 2, Name = "Tørkerapport" },
            };
        }

        static IEnumerable<Mission> GetPreconfiguredMissions()
        {
            var missions = new List<Mission>();
            for (var i = 1; i <= 300; i++)
            {
                missions.Add(new Mission()
                {
                    Id = i,
                    Name = $"Oppdrag {i}",
                    PhoneNumber = "92278483",
                    Description = "Dette er en beskrivelse",
                    Address = $"Furuberget {i}, 1940 Bjørkelangen",
                    EmployerId = random.Next(1,6),
                    MissionTypeId = i % 2 == 0 ? 1 : 2,
                });
            }

            return missions;
        }
        static IEnumerable<MissionReport> GetPreconfiguredMissionReports()
        {
            var missionReports = new List<MissionReport>();

            for (var i = 1; i <= 500; i++)
            {
                missionReports.Add(new MissionReport()
                {
                    Id = i,
                    FileURL = new Uri("https://kbtv.blob.core.windows.net/images/28e89dfa-8d9b-422f-81fd-ee1f7aafbbe7.jpg"),
                    MissionId = random.Next(1, 300),
                    ReportTypeId = i % 2 == 0 ? 1 : 2
                });
            }
     
            return missionReports;
        }

        static IEnumerable<MissionNote> GetPreconfiguredMissionNotes()
        {
            var missionNotes = new List<MissionNote>();

            for (var i = 1; i <= 500; i++)
            {
                    missionNotes.Add(new MissionNote()
                    {
                        Id = i,
                        MissionId = random.Next(1, 300),
                        Content = "Dette er ett veldig interessant notat. Veldig interessant. Det er også veldig viktig.",
                        Title =  "Dette er ett notat",
                        Pinned = i % 2 == 0 ? false : true
                    });           
            }

            return missionNotes;
        }

        static IEnumerable<Timesheet> GetPreconfiguredTimesheets()
        {
            var timesheets = new List<Timesheet>();
            var idCounter = 1;
            var dayCounter = 0;
            var today = DateTime.Now.Date.AddHours(6);

            for (var n = 1; n <= 3000; n++)
            {        
                    var startDate = today.AddDays(-dayCounter);
                    var endDate = startDate.AddHours(random.Next(4, 10));
                    timesheets.Add(new Timesheet()
                    {
                        Id = idCounter,
                        MissionId = random.Next(1,300),
                        StartTime = startDate,
                        EndTime = endDate,
                        TotalHours = (endDate - startDate).TotalHours,
                        Comment = "Dette er en kommentar til en time for leder for ett kult oppdrag",
                        UserName = "leder",
                        Status = n % 2 == 0 ? TimesheetStatus.Open : TimesheetStatus.Confirmed
                    });
                    idCounter++;

                    for(var user = 0; user <= 3; user++)
                    {
                        timesheets.Add(new Timesheet()
                        {
                            Id = idCounter,
                            MissionId = random.Next(1, 300),
                            StartTime = startDate,
                            EndTime = endDate,
                            TotalHours = (endDate - startDate).TotalHours,
                            Comment = "Dette er en kommentar til en time for ansatt",
                            UserName = "ansatt" + user,
                            Status = n % 2 == 0 ? TimesheetStatus.Open : TimesheetStatus.Confirmed
                        });
                        idCounter++;
                    }
     

                    timesheets.Add(new Timesheet()
                    {
                        Id = idCounter,
                        MissionId = random.Next(1, 300),
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



            return timesheets;
        }
    }
 
}
