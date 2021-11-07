using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core;
using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BjBygg.Infrastructure.Data
{
    class ModelEntry { 
        public string Id { get; set; }
        public long Timestamp { get; set; } 
    }

    public class AppDbContextSeed
    {
        private static Dictionary<Type, List<ModelEntry>> _generatedIds = new Dictionary<Type, List<ModelEntry>>();
        private static Random rnd = new Random();

        private static string[] postals = { "1940 Bjørkelangen", "3187 Horten", "Oslo", "Sarpsborg", "0619 Ålesund", "6214 Norddal" };
        private static string[] areas = { "Furuberget", "Redaktør Thommessens gate", "Fernanda Nissens Gate", "Karl Johans gate", "Reddal",
                "Moa", "Spjelkavik", "Tjøme", };

        public static async Task SeedAllAsync(IAppDbContext context, IIdGenerator idGenerator, SeederCount seederCount)
        {
            if (!context.Employers.Any())
                await SetEmployersAsync(context, idGenerator);
            if (!context.EmployerUsers.Any())
                context.EmployerUsers.Add(
                     new EmployerUser() { Id = "dasdsad", EmployerId = GetGeneratedId(typeof(Employer), DateTime.Now), UserName = "Oppdragsgiver" }
                 ); 
            if (!context.Activities.Any())
                await SetActivitiesAsync(context, idGenerator);
            if (!context.Missions.Any())
                await SetMissionsAsync(context, idGenerator, seederCount.SeedCounts[typeof(Mission)]);
            if (!context.MissionActivities.Any())
                await SetMissionActivitiesAsync(context, idGenerator);
            if (!context.MissionDocuments.Any())
                await SetMissionDocumentsAsync(context, idGenerator, seederCount.SeedCounts[typeof(MissionDocument)]);
            if (!context.MissionImages.Any())
                await SetMissionImagesAsync(context, idGenerator, seederCount.SeedCounts[typeof(MissionImage)]);
            if (!context.MissionNotes.Any())
                await SetMissionNotesAsync(context, idGenerator, seederCount.SeedCounts[typeof(MissionNote)]);
            if (!context.Timesheets.Any())
                await SetTimesheetsAsync(context, idGenerator, seederCount.SeedCounts[typeof(Timesheet)]);
        }

        static void AddGeneratedId(string id, Type type, DateTime date)
        {
            var entry = new ModelEntry() { Id = id, Timestamp = DateTimeHelper.ConvertDateToEpoch(date) };
            if (!_generatedIds.ContainsKey(type)) _generatedIds.Add(type, new List<ModelEntry> { entry });
            else _generatedIds[type].Add(entry);
        }

        static string GetGeneratedId(Type type, DateTime? date = null)
        {
            var entries = _generatedIds[type];
            if (entries == null || entries.Count == 0) throw new Exception($"No ids for type {type} when seeding. Check seeding order.");
            if (date == null) return entries[rnd.Next(entries.Count - 1)].Id;
            var timestamp = DateTimeHelper.ConvertDateToEpoch((DateTime) date);
            var closest = entries.Aggregate((x, y) => Math.Abs(x.Timestamp - timestamp) < Math.Abs(y.Timestamp - timestamp) ? x : y);
            return closest.Id;
        }

        static async Task SetEmployersAsync(IAppDbContext context, IIdGenerator idGenerator)
        {
            string[] companies = { "NSU AS", "RSU AS", "FSU AS", "Finsrup AS" };
            var command = "INSERT INTO Employers (Id, Name, Email, Address, PhoneNumber, Deleted, CreatedAt, UpdatedAt) VALUES ";
            
            foreach(var entry in companies.Select((value, i) => new { i, value }))
            {
                var id = idGenerator.Generate();
                var date = DateTimeHelper.Now().AddMonths(-entry.i);
                var dateFormatted = date.ToString("yyyy-MM-dd HH:mm:ss");
                AddGeneratedId(id, typeof(Employer), date);
                command = String.Concat(command, $"('{id}', '{entry.value}', 'ivar@eksempel.no', '{getAddress()}', '92278489', 0, '{dateFormatted}', '{dateFormatted}')");
                if (entry.value != companies.Last()) command = String.Concat(command, ",");
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetActivitiesAsync(IAppDbContext context, IIdGenerator idGenerator)
        {
            string[] activities = { "Riving", "Legge gulv", "Rengjøre", "Administrering", "Maling" };
            var command = "INSERT INTO Activities (Id, Name, Deleted, CreatedAt, UpdatedAt) VALUES ";
            foreach(var entry in activities.Select((value, i) => new { i, value }))
            {
                var id = idGenerator.Generate();
                var date = DateTimeHelper.Now().AddMonths(-entry.i);
                var dateFormatted = date.ToString("yyyy-MM-dd HH:mm:ss");
                AddGeneratedId(id, typeof(Activity), date);
                command = String.Concat(command, $"('{id}', '{entry.value}', 0, '{dateFormatted}', '{dateFormatted}')");
                if (entry.value != activities.Last()) command = String.Concat(command, ",");
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionsAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO Missions (Id, Address, PhoneNumber, Description, EmployerId, " +
                "FileName, Position_Latitude, Position_Longitude, Position_IsExact, Deleted, CreatedAt, UpdatedAt) VALUES ";

            string[] images = {
                "sample1_ratio=1.9.jpg",
                "sample2_ratio=1.67.jpg",
                "sample3_ratio=1.jpg",
                "sample4_ratio=1.47.jpg",
                "sample5_ratio=1.51.jpg",
                "sample6_ratio=1.5.jpg",
            };

            for (var i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                var date = DateTimeHelper.Now().AddDays(-i);
                var dateFormatted = date.ToString("yyyy-MM-dd HH:mm:ss");
                AddGeneratedId(id, typeof(Mission), date);
                var image = images[rnd.Next(0, images.Length)];
                var employerId = GetGeneratedId(typeof(Employer), date);
                var positionLatitude = 58 + rnd.NextDouble() * 4; 
                var positionLongitude = 8 + rnd.NextDouble() * 4;
                var isExact = i % 2 == 0 ? 0 : 1;
                command = String.Concat(command, $"('{id}', '{ getAddress() }', '92278489', 'Røykskade i 2.etasje', '{employerId}', '{image}'," +
                    $"{positionLatitude}, {positionLongitude}, {isExact}, 0, '{dateFormatted}', '{dateFormatted}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
      
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionActivitiesAsync(IAppDbContext context, IIdGenerator idGenerator)
        {
            var command = "INSERT INTO MissionActivities (Id, MissionId, ActivityId, Deleted, CreatedAt, UpdatedAt) VALUES ";
            var missionEntries = _generatedIds[typeof(Mission)];
            for(var m = 0; m < missionEntries.Count; m++)
            {
                var entry = missionEntries[m];
                var count = rnd.Next(1);
                for (int i = 0; i <= count; i++)
                {
                    var id = idGenerator.Generate();
                    var date = DateTimeHelper.Now().AddDays(-m);
                    var dateFormatted = date.ToString("yyyy-MM-dd HH:mm:ss");
                    AddGeneratedId(id, typeof(MissionActivity), date);
                    var activityId = GetGeneratedId(typeof(Activity), date);
                    command = String.Concat(command, $"('{id}', '{entry.Id}', '{activityId}', 0, '{dateFormatted}', '{dateFormatted}')");
                    if (i != count) command = String.Concat(command, ",");
                }
                if (m != (missionEntries.Count - 1)) command = String.Concat(command, ",");
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionDocumentsAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO MissionDocuments (Id, FileName, MissionId, Name, Deleted, CreatedAt, UpdatedAt) VALUES ";
            string[] documents = {
                "sample1.pdf",
                "sample2.txt",
                "sample3.docx",
                "sample4.pdf",
                "sample5.pdf"
            };
            for (var i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                var date = DateTimeHelper.Now().AddDays(-i);
                var dateFormatted = date.ToString("yyyy-MM-dd HH:mm:ss");
                AddGeneratedId(id, typeof(MissionDocument), date);
                var document = documents[rnd.Next(0, documents.Length)];
                var missionId = GetGeneratedId(typeof(Mission));
                command = String.Concat(command,
                    $"('{id}', '{document}', '{missionId}', 'Skaderapport{i}', 0, '{dateFormatted}', '{dateFormatted}')");

                if (i < (amount - 1)) command = String.Concat(command, ",");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionImagesAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO MissionImages (Id, FileName, MissionId, Deleted, CreatedAt, UpdatedAt) VALUES ";
            string[] images = {
                "sample1_ratio=1.33.jpg",
                "sample2_ratio=1.33.jpg",
                "sample3_ratio=1.33.jpg",
                "sample4_ratio=1.33.jpg",
                "sample5_ratio=1.78.jpg",
                "sample6_ratio=1.78.jpg",
                "sample7_ratio=1.25.jpg",
                "sample8_ratio=1.25.jpg",
            };
            for (var i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                var date = DateTimeHelper.Now().AddDays(-i);
                var dateFormatted = date.ToString("yyyy-MM-dd HH:mm:ss");
                AddGeneratedId(id, typeof(MissionImage), date);
                var image = images[rnd.Next(0, images.Length)];
                var missionId = GetGeneratedId(typeof(Mission));
                command =
                    String.Concat(command, $"('{id}', '{image}', '{missionId}', 0, '{dateFormatted}', '{dateFormatted}')");

                if (i < (amount - 1)) command = String.Concat(command, ",");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionNotesAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO MissionNotes (Id, Content, MissionId, Deleted, CreatedAt, UpdatedAt) VALUES ";

            for (var i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                var date = DateTimeHelper.Now().AddDays(-i);
                var dateFormatted = date.ToString("yyyy-MM-dd HH:mm:ss");
                AddGeneratedId(id, typeof(MissionNote), date);
                var missionId = GetGeneratedId(typeof(Mission));
                command =
                    String.Concat(command, $"('{id}', 'testnotat', '{missionId}', 0, '{dateFormatted}', '{dateFormatted}')");

                if (i < (amount - 1)) command = String.Concat(command, ",");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetTimesheetsAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO Timesheets " +
                "(Id, MissionActivityId, StartTime, EndTime, TotalHours, Comment, UserName, Status, Deleted, CreatedAt, UpdatedAt) " +
                "VALUES ";

            var today = DateTimeHelper.Now();

            string[] users = { Roles.Leader, Roles.Employee, Roles.Management };

            for (var i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                var startDate = today.AddDays(-i) + new TimeSpan(7, 0, 0);
                var endDate = startDate.AddHours(rnd.Next(4, 10));
                var totalHours = (endDate - startDate).TotalHours;
                var startDateString = startDate.ToString("yyyy-MM-dd HH:mm:ss");
                var endDateString = endDate.ToString("yyyy-MM-dd HH:mm:ss");
                AddGeneratedId(id, typeof(Timesheet), startDate);
                var status = i % 2 == 0 ? 1 : 2;
                var missionActivityId = GetGeneratedId(typeof(MissionActivity), startDate);
                var userName = users[rnd.Next(0, users.Length)];
                command = String.Concat(command,
                $"('{id}', '{missionActivityId}', '{startDateString}','{endDateString}',{totalHours}, 'test', '{userName}', {status}, 0, '{startDateString}', '{startDateString}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        private static string getAddress()
        {
            return $"{areas[rnd.Next(0, areas.Length)]} {rnd.Next(0, 50)}, {postals[rnd.Next(0, postals.Length)]}";
        }
    }

}
