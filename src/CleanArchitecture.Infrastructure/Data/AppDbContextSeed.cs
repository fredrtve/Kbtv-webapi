using BjBygg.Application.Application.Common.Interfaces;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        readonly static Random random = new Random();
        private static int _numberOfMissions;
        public static void Seed(IAppDbContext context, int numberOfMissions)
        {
            _numberOfMissions = numberOfMissions;
            try
            {
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
                if (!context.DocumentTypes.Any())
                {
                    context.Database.OpenConnection();
                    context.DocumentTypes.AddRange(
                        GetPreconfiguredDocumentTypes());

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
                if (!context.MissionDocuments.Any())
                {
                    context.Database.OpenConnection();
                    context.MissionDocuments.AddRange(
                        GetPreconfiguredMissionDocuments());

                    context.SaveChanges();
                    context.Database.CloseConnection();
                }
                if (!context.MissionImages.Any())
                {
                    context.Database.OpenConnection();
                    context.MissionImages.AddRange(
                        GetPreconfiguredMissionImages());

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
                new Employer() { Id = 7, Name = "NSU2", Email = "NSU2@test.no" },
                new Employer() { Id = 8, Name = "BSU2", Email = "BSU2@test.no" },
                new Employer() { Id = 9, Name = "RSU2", Email = "RSU2@test.no" },
                new Employer() { Id = 10, Name = "TSU2", Email = "TSU2@test.no" },
                new Employer() { Id = 11, Name = "Privat2", Email = "Privat2@test.no" },
            };
        }
        static IEnumerable<MissionType> GetPreconfiguredMissionTypes()
        {
            return new List<MissionType>()
            {
                new MissionType() { Id = 1, Name = "Riving" },
                new MissionType() { Id = 2, Name = "Oppbygging" },
                new MissionType() { Id = 3, Name = "Riving2" },
                new MissionType() { Id = 4, Name = "Oppbygging2" },
                new MissionType() { Id = 5, Name = "Riving3" },
                new MissionType() { Id = 6, Name = "Oppbygging3" },
                new MissionType() { Id = 7, Name = "Oppbygging4" },
                new MissionType() { Id = 8, Name = "Riving4" },
                new MissionType() { Id = 9, Name = "Oppbygging5" },
                new MissionType() { Id = 10, Name = "Riving5" },
            };
        }

        static IEnumerable<DocumentType> GetPreconfiguredDocumentTypes()
        {
            return new List<DocumentType>()
            {
                new DocumentType() { Id = 1, Name = "Skaderapport" },
                new DocumentType() { Id = 2, Name = "Tørkerapport" },
                new DocumentType() { Id = 3, Name = "Skaderapport1" },
                new DocumentType() { Id = 4, Name = "Tørkerapport1" },
                new DocumentType() { Id = 5, Name = "Skaderapport2" },
                new DocumentType() { Id = 6, Name = "Tørkerapport2" },
                new DocumentType() { Id = 7, Name = "Skaderapport3" },
                new DocumentType() { Id = 8, Name = "Tørkerapport3" },
            };
        }

        static IEnumerable<Mission> GetPreconfiguredMissions()
        {
            var missions = new List<Mission>();
            for (var i = 1; i <= _numberOfMissions; i++)
            {
                missions.Add(new Mission()
                {
                    Id = i,
                    Name = $"Oppdrag {i}",
                    PhoneNumber = "92278483",
                    Description = "Dette er en beskrivelse",
                    Address = $"Furuberget {i}, 1940 Bjørkelangen",
                    EmployerId = random.Next(1, 6),
                    MissionTypeId = i % 2 == 0 ? 1 : 2,
                });
            }

            return missions;
        }
        static IEnumerable<MissionDocument> GetPreconfiguredMissionDocuments()
        {
            var missionDocuments = new List<MissionDocument>();

            for (var i = 1; i <= _numberOfMissions * 2; i++)
            {
                missionDocuments.Add(new MissionDocument()
                {
                    Id = i,
                    FileURL = new Uri("https://kbtv.blob.core.windows.net/images/28e89dfa-8d9b-422f-81fd-ee1f7aafbbe7.jpg"),
                    MissionId = random.Next(1, _numberOfMissions),
                    DocumentTypeId = i % 2 == 0 ? 1 : 2
                });
            }

            return missionDocuments;
        }
        static IEnumerable<MissionImage> GetPreconfiguredMissionImages()
        {
            var missionImages = new List<MissionImage>();
            var imageUrls = new List<Uri>
            {
                new Uri("https://kbtv.blob.core.windows.net/images/1637271568376872507_90030357-a167-426d-8ca2-fd669e17888b.jpg"),
                new Uri("https://kbtv.blob.core.windows.net/images/1637271568378142015_fef915f9-5f35-45ea-96ae-898f33d79df2.jpg"),
                new Uri("https://kbtv.blob.core.windows.net/images/1637125277915871387_33a58ce3-9b65-42c1-99aa-35bbbf200e82.jpg")
            };
            for (var i = 1; i <= _numberOfMissions * 4; i++)
            {
                missionImages.Add(new MissionImage()
                {
                    Id = i,
                    MissionId = random.Next(1, _numberOfMissions),
                    FileURL = imageUrls[random.Next(imageUrls.Count)]
                });
            }

            return missionImages;
        }


        static IEnumerable<MissionNote> GetPreconfiguredMissionNotes()
        {
            var missionNotes = new List<MissionNote>();

            for (var i = 1; i <= _numberOfMissions; i++)
            {
                missionNotes.Add(new MissionNote()
                {
                    Id = i,
                    MissionId = random.Next(1, _numberOfMissions),
                    Content = "Dette er ett veldig interessant notat. Veldig interessant. Det er også veldig viktig.",
                    Title = "Dette er ett notat",
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
            var today = DateTime.UtcNow.Date.AddHours(6);

            for (var n = 1; n <= _numberOfMissions * 6; n++)
            {
                var startDate = today.AddDays(-dayCounter);
                var endDate = startDate.AddHours(random.Next(4, 10));
                timesheets.Add(new Timesheet()
                {
                    Id = idCounter,
                    MissionId = random.Next(1, _numberOfMissions),
                    StartTime = startDate,
                    EndTime = endDate,
                    TotalHours = (endDate - startDate).TotalHours,
                    Comment = "Dette er en kommentar til en time for leder for ett kult oppdrag",
                    UserName = "leder",
                    Status = n % 2 == 0 ? TimesheetStatus.Open : TimesheetStatus.Confirmed
                });
                idCounter++;

                for (var user = 0; user <= 3; user++)
                {
                    timesheets.Add(new Timesheet()
                    {
                        Id = idCounter,
                        MissionId = random.Next(1, _numberOfMissions),
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
                    MissionId = random.Next(1, _numberOfMissions),
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
