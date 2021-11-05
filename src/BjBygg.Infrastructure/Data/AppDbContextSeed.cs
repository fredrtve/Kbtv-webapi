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
    public class AppDbContextSeed
    {
        private static Dictionary<Type, List<string>> _generatedIds = new Dictionary<Type, List<string>>();
        private static Random rnd = new Random();

        private static string[] postals = { "1940 Bjørkelangen", "3187 Horten", "Oslo", "Sarpsborg", "0619 Ålesund", "6214 Norddal" };
        private static string[] areas = { "Furuberget", "Redaktør Thommessens gate", "Fernanda Nissens Gate", "Karl Johans gate", "Reddal",
                "Moa", "Spjelkavik", "Tjøme", };

        public static async Task SeedAllAsync(IAppDbContext context, IIdGenerator idGenerator, SeederCount seederCount)
        {
            if (!context.Employers.Any())
                await SetEmployersAsync(context, idGenerator, seederCount.SeedCounts[typeof(Employer)]);
            if (!context.EmployerUsers.Any())
                context.EmployerUsers.Add(
                     new EmployerUser() { Id = "dasdsad", EmployerId = GetGeneratedId(typeof(Employer)), UserName = "Oppdragsgiver" }
                 ); 
            if (!context.Activities.Any())
                await SetActivitiesAsync(context, idGenerator, seederCount.SeedCounts[typeof(Activity)]);
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

        static void AddGeneratedId(string id, Type type)
        {
            if (!_generatedIds.ContainsKey(type)) _generatedIds.Add(type, new List<string> { id });
            else _generatedIds[type].Add(id);
        }

        static string GetGeneratedId(Type type)
        {
            var ids = _generatedIds[type];
            if (ids == null || ids.Count == 0) throw new Exception($"No ids for type {type} when seeding. Check seeding order.");
            return ids[rnd.Next(ids.Count - 1)];
        }

        static async Task SetEmployersAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            string[] companies = {
                "NSU AS",
                "RSU AS",
                "FSU AS",
                "Finsrup AS"
            };
            var command = "INSERT INTO Employers (Id, Name, Email, Address, PhoneNumber, Deleted, CreatedAt, UpdatedAt) VALUES ";
            
            for (int i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                AddGeneratedId(id, typeof(Employer));
                var company = companies[rnd.Next(0, companies.Length)];
                var date = DateTimeHelper.Now().AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                command = String.Concat(command, $"('{id}', '{company}', 'ivar@eksempel.no', '{getAddress(i)}', '92278489', 0, '{date}', '{date}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetActivitiesAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            string[] activities = { "Riving", "Legge gulv", "Rengjøre", "Administrering", "Maling", "Annet"};
            var command = "INSERT INTO Activities (Id, Name, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (int i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                AddGeneratedId(id, typeof(Activity));
                var type = activities[rnd.Next(0, activities.Length)];
                var date = DateTimeHelper.Now().AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                command = String.Concat(command, $"('{id}', '{type}', 0, '{date}', '{date}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
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
                AddGeneratedId(id, typeof(Mission));
                var image = images[rnd.Next(0, images.Length)];
                var employerId = GetGeneratedId(typeof(Employer));
                var date = DateTimeHelper.Now().AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                var positionLatitude = 58 + rnd.NextDouble() * 4; 
                var positionLongitude = 8 + rnd.NextDouble() * 4;
                var isExact = i % 2 == 0 ? 0 : 1;
                command = String.Concat(command, $"('{id}', '{ getAddress(i) }', '92278489', 'Røykskade i 2.etasje', '{employerId}', '{image}'," +
                    $"{positionLatitude}, {positionLongitude}, {isExact}, 0, '{date}', '{date}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
      
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionActivitiesAsync(IAppDbContext context, IIdGenerator idGenerator)
        {
            var command = "INSERT INTO MissionActivities (Id, MissionId, ActivityId, Deleted, CreatedAt, UpdatedAt) VALUES ";
            var missionIds = _generatedIds[typeof(Mission)];
            for(var m = 0; m < missionIds.Count; m++)
            {
                var missionId = missionIds[m];
                var count = rnd.Next(1);
                for (int i = 0; i <= count; i++)
                {
                    var id = idGenerator.Generate(); 
                    AddGeneratedId(id, typeof(MissionActivity));
                    var activityId = GetGeneratedId(typeof(Activity));
                    var date = DateTimeHelper.Now().AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                    command = String.Concat(command, $"('{id}', '{missionId}', '{activityId}', 0, '{date}', '{date}')");
                    if (i != count) command = String.Concat(command, ",");
                }
                if (m != (missionIds.Count - 1)) command = String.Concat(command, ",");
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
                AddGeneratedId(id, typeof(MissionDocument));
                var date = DateTimeHelper.Now().AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                var document = documents[rnd.Next(0, documents.Length)];
                var missionId = GetGeneratedId(typeof(Mission));
                command = String.Concat(command,
                    $"('{id}', '{document}', '{missionId}', 'Skaderapport{i}', 0, '{date}', '{date}')");

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
                AddGeneratedId(id, typeof(MissionImage));
                var date = DateTimeHelper.Now().AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                var image = images[rnd.Next(0, images.Length)];
                var missionId = GetGeneratedId(typeof(Mission));
                command =
                    String.Concat(command, $"('{id}', '{image}', '{missionId}', 0, '{date}', '{date}')");

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
                AddGeneratedId(id, typeof(MissionNote));
                var date = DateTimeHelper.Now().AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                var missionId = GetGeneratedId(typeof(Mission));
                command =
                    String.Concat(command, $"('{id}', 'testnotat', '{missionId}', 0, '{date}', '{date}')");

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
                AddGeneratedId(id, typeof(Timesheet));
                var startDate = today.AddDays(-i) + new TimeSpan(7, 0, 0);
                var endDate = startDate.AddHours(rnd.Next(4, 10));
                var totalHours = (endDate - startDate).TotalHours;
                var startDateString = startDate.ToString("yyyy-MM-dd HH:mm:ss");
                var endDateString = endDate.ToString("yyyy-MM-dd HH:mm:ss");
                var status = i % 2 == 0 ? 1 : 2;
                var missionActivityId = GetGeneratedId(typeof(MissionActivity));
                var userName = users[rnd.Next(0, users.Length)];
                command = String.Concat(command,
                $"('{id}', '{missionActivityId}', '{startDateString}','{endDateString}',{totalHours}, 'test', '{userName}', {status}, 0, '{startDateString}', '{startDateString}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        private static string getAddress(int index)
        {
            return $"{areas[rnd.Next(0, areas.Length)]} {index}, {postals[rnd.Next(0, postals.Length)]}";
        }
    }

}
