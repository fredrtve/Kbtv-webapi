using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    public class TestSeeder
    {

        public static async Task SeedAllAsync(AppDbContext context, TestSeederConfig config)
        {
            using (var ctx = context)
            {
                await SetEmployersAsync(ctx, config.Employers);
                await SetMissionTypesAsync(ctx, config.MissionTypes);
                await SetDocumentTypesAsync(ctx, config.DocumentTypes);
                await SetMissionsAsync(ctx, config.Missions);
                await SetMissionDocumentsAsync(ctx, config.MissionDocuments);
                await SetMissionImagesAsync(ctx, config.MissionImages);
                await SetMissionNotesAsync(ctx, config.MissionNotes);
                await SetTimesheetsAsync(ctx, config.Timesheets);
            }
        }

        static async Task SetEmployersAsync(AppDbContext context, int amount)
        {
            var command = "INSERT INTO Employers (Name, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for(int i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                if (i < (amount - 1)) command = String.Concat(command, $"('NSU{i}', 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('NSU{i}', 0, '{date}', '{date}')");
            }
 

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionTypesAsync(AppDbContext context, int amount)
        {
            var command = "INSERT INTO MissionTypes (Name, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (int i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                if (i < (amount - 1)) command = String.Concat(command, $"('Riving{i}', 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('Riving{i}', 0, '{date}', '{date}')");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetDocumentTypesAsync(AppDbContext context, int amount)
        {
            var command = "INSERT INTO DocumentTypes (Name, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (int i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                if (i < (amount - 1)) command = String.Concat(command, $"('Skaderapport{i}', 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('Skaderapport{i}', 0, '{date}', '{date}')");
            }
            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionsAsync(AppDbContext context, int amount)
        {
            var command = "INSERT INTO Missions (Address, EmployerId, MissionTypeId, Deleted, CreatedAt, UpdatedAt) VALUES ";

            for (var i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i*2).ToString("yyyy-MM-dd HH:mm:ss");
                if(i < (amount - 1)) command = String.Concat(command, $"('Furuberget {i}, 1940 Bjørkelangen', 1, 1, 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('Furuberget {i}, 1940 Bjørkelangen', NULL, 1, 0, '{date}', '{date}')");
            }

            await context.Database.ExecuteSqlRawAsync(command); 
        }
        static async Task SetMissionDocumentsAsync(AppDbContext context, int amount)
        {
            var command = "INSERT INTO MissionDocuments (FileURL, MissionId, DocumentTypeId, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (var i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                if (i < (amount-1)) command = String.Concat(command, $"('https://test.com', 1, 1, 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('https://test.com', 1, 1, 0, '{date}', '{date}')");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionImagesAsync(AppDbContext context, int amount)
        {
            var command = "INSERT INTO MissionImages (FileURL, MissionId, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (var i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                if (i < (amount - 1)) command = String.Concat(command, $"('https://test.com', 1, 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('https://test.com', 1, 0, '{date}', '{date}')");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionNotesAsync(AppDbContext context, int amount)
        {
            var command = "INSERT INTO MissionNotes (Content, MissionId, Pinned, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (var i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                var pinned = i % 2 == 0 ? 0 : 1;
                if (i < (amount - 1)) command = String.Concat(command, $"('testnotat', 1, {pinned}, 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('testnotat', 1, {pinned}, 0, '{date}', '{date}')");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetTimesheetsAsync(AppDbContext context, int amount)
        {
            var command = "INSERT INTO Timesheets " +
                "(MissionId, StartTime, EndTime, TotalHours, Comment, UserName, Status, Deleted, CreatedAt, UpdatedAt) " +
                "VALUES ";

            var today = DateTime.UtcNow.Date.AddHours(6);

            for(var d = 0; d < amount; d++)
            {
                var startDate = today.AddDays(-d);
                var endDate = startDate.AddHours(4);
                var totalHours = (endDate - startDate).TotalHours;
                var startDateString = startDate.ToString("yyyy-MM-dd HH:mm:ss");
                var endDateString = endDate.ToString("yyyy-MM-dd HH:mm:ss");
                var status = d % 2 == 0 ? 0 : 2;
                if (d < (amount - 1))
                    command = String.Concat(command,
                    $"(1, '{startDateString}','{endDateString}',{totalHours}, 'test', 'leder', {status}, 0, '{startDateString}', '{startDateString}'),");
                else
                    command = String.Concat(command,
                    $"(1, '{startDateString}','{endDateString}',{totalHours}, 'test', 'mellomleder', {status}, 0, '{startDateString}', '{startDateString}')");             
            }
        
            await context.Database.ExecuteSqlRawAsync(command);
        }
    }
}
