using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Data
{
    public class AppDbContextSeed
    {
        private static Dictionary<Type, List<string>> _generatedIds = new Dictionary<Type, List<string>>();
        private static Random rnd = new Random();

        public static async Task SeedAllAsync(IAppDbContext context, IIdGenerator idGenerator, SeederCount seederCount)
        {
            using var ctx = context;
            await SetEmployersAsync(ctx, idGenerator, seederCount.SeedCounts[typeof(Employer)]);
            await SetMissionTypesAsync(ctx, idGenerator, seederCount.SeedCounts[typeof(MissionType)]);
            await SetDocumentTypesAsync(ctx, idGenerator, seederCount.SeedCounts[typeof(DocumentType)]);
            await SetMissionsAsync(ctx, idGenerator, seederCount.SeedCounts[typeof(Mission)]);
            await SetMissionDocumentsAsync(ctx, idGenerator, seederCount.SeedCounts[typeof(MissionDocument)]);
            await SetMissionImagesAsync(ctx, idGenerator, seederCount.SeedCounts[typeof(MissionImage)]);
            await SetMissionNotesAsync(ctx, idGenerator, seederCount.SeedCounts[typeof(MissionNote)]);
            await SetTimesheetsAsync(ctx, idGenerator, seederCount.SeedCounts[typeof(Timesheet)]);
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
            return ids[rnd.Next(ids.Count)];
        }

        static async Task SetEmployersAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO Employers (Id, Name, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (int i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                AddGeneratedId(id, typeof(Employer));
                var date = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                command = String.Concat(command, $"('{id}', 'NSU{i}', 0, '{date}', '{date}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionTypesAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO MissionTypes (Id, Name, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (int i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                AddGeneratedId(id, typeof(MissionType));
                var date = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                command = String.Concat(command, $"('{id}', 'Riving{i}', 0, '{date}', '{date}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetDocumentTypesAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO DocumentTypes (Id, Name, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (int i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                AddGeneratedId(id, typeof(DocumentType));
                var date = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                command = String.Concat(command, $"('{id}', 'Skaderapport{i}', 0, '{date}', '{date}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionsAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO Missions (Id, Address, EmployerId, MissionTypeId, Deleted, CreatedAt, UpdatedAt) VALUES ";

            for (var i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                AddGeneratedId(id, typeof(Mission));
                var employerId = GetGeneratedId(typeof(Employer));
                var typeId = GetGeneratedId(typeof(MissionType));
                var date = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                command = String.Concat(command, $"('{id}', 'Furuberget {i}, 1940 Bjørkelangen', '{employerId}', '{typeId}', 0, '{date}', '{date}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionDocumentsAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO MissionDocuments (Id, FileName, MissionId, DocumentTypeId, Deleted, CreatedAt, UpdatedAt) VALUES ";
            string[] documents = { 
                "1637271568378142015_fef915f9-5f35-45ea-96ae-898f33d79df2.jpg",
                "1637125277915871387_33a58ce3-9b65-42c1-99aa-35bbbf200e82.jpg",
                "1637271568376872507_90030357-a167-426d-8ca2-fd669e17888b.jpg",
            };
            for (var i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                AddGeneratedId(id, typeof(MissionDocument));
                var date = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                var document = documents[rnd.Next(0, documents.Length)];
                var typeId = GetGeneratedId(typeof(DocumentType));
                var missionId = GetGeneratedId(typeof(Mission));
                command = String.Concat(command, 
                    $"('{id}', '{document}', '{missionId}', '{typeId}', 0, '{date}', '{date}')");

                if (i < (amount - 1)) command = String.Concat(command, ",");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionImagesAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO MissionImages (Id, FileName, MissionId, Deleted, CreatedAt, UpdatedAt) VALUES ";
            string[] images = {
                "1637271568378142015_fef915f9-5f35-45ea-96ae-898f33d79df2.jpg",
                "1637125277915871387_33a58ce3-9b65-42c1-99aa-35bbbf200e82.jpg",
                "1637271568376872507_90030357-a167-426d-8ca2-fd669e17888b.jpg",
            };
            for (var i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                AddGeneratedId(id, typeof(MissionImage));
                var date = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
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
            var command = "INSERT INTO MissionNotes (Id, Content, MissionId, Pinned, Deleted, CreatedAt, UpdatedAt) VALUES ";

            for (var i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                AddGeneratedId(id, typeof(MissionNote));
                var date = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd HH:mm:ss");
                var pinned = i % 2 == 0 ? 0 : 1;
                var missionId = GetGeneratedId(typeof(Mission));
                command = 
                    String.Concat(command, $"('{id}', 'testnotat', '{missionId}', {pinned}, 0, '{date}', '{date}')");

                if (i < (amount - 1)) command = String.Concat(command, ",");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetTimesheetsAsync(IAppDbContext context, IIdGenerator idGenerator, int amount)
        {
            var command = "INSERT INTO Timesheets " +
                "(Id, MissionId, StartTime, EndTime, TotalHours, Comment, UserName, Status, Deleted, CreatedAt, UpdatedAt) " +
                "VALUES ";

            var today = DateTimeHelper.Now();

            string[] users = {Roles.Leader, Roles.Employee, Roles.Management};

            for (var i = 0; i < amount; i++)
            {
                var id = idGenerator.Generate();
                AddGeneratedId(id, typeof(Timesheet));
                var startDate = today.AddDays(-i);
                var endDate = startDate.AddHours(rnd.Next(4, 10));
                var totalHours = (endDate - startDate).TotalHours;
                var startDateString = startDate.ToString("yyyy-MM-dd HH:mm:ss");
                var endDateString = endDate.ToString("yyyy-MM-dd HH:mm:ss");
                var status = i % 2 == 0 ? 1 : 2;
                var missionId = GetGeneratedId(typeof(Mission));
                var userName = users[rnd.Next(0, users.Length)];
                command = String.Concat(command,
                $"('{id}', '{missionId}', '{startDateString}','{endDateString}',{totalHours}, 'test', '{userName}', {status}, 0, '{startDateString}', '{startDateString}')");
                if (i < (amount - 1)) command = String.Concat(command, ",");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }

    }

}
