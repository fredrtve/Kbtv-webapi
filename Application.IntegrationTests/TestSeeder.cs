using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    public class TestSeeder
    {

        public static async Task SeedAllAsync(IAppDbContext context, TestSeederCount seederCount)
        {
            using (var ctx = context)
            {
                await SetEmployersAsync(ctx, seederCount.SeedCounts[typeof(Employer)]);
                await SetMissionTypesAsync(ctx, seederCount.SeedCounts[typeof(MissionType)]);
                await SetDocumentTypesAsync(ctx, seederCount.SeedCounts[typeof(DocumentType)]);
                await SetMissionsAsync(ctx, seederCount.SeedCounts[typeof(Mission)]);
                await SetMissionDocumentsAsync(ctx, seederCount.SeedCounts[typeof(MissionDocument)]);
                await SetMissionImagesAsync(ctx, seederCount.SeedCounts[typeof(MissionImage)]);
                await SetMissionNotesAsync(ctx, seederCount.SeedCounts[typeof(MissionNote)]);
                await SetTimesheetsAsync(ctx, seederCount.SeedCounts[typeof(Timesheet)]);
            }
        }

        static async Task SetEmployersAsync(IAppDbContext context, int amount)
        {
            var command = "INSERT INTO Employers (Name, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (int i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                if (i < (amount - 1)) command = String.Concat(command, $"('NSU{i}', 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('NSU{i}', 0, '{date}', '{date}')");
            }


            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionTypesAsync(IAppDbContext context, int amount)
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
        static async Task SetDocumentTypesAsync(IAppDbContext context, int amount)
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
        static async Task SetMissionsAsync(IAppDbContext context, int amount)
        {
            var command = "INSERT INTO Missions (Address, EmployerId, MissionTypeId, Deleted, CreatedAt, UpdatedAt) VALUES ";

            for (var i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                if (i < (amount - 1)) command = String.Concat(command, $"('Furuberget {i}, 1940 Bjørkelangen', {i + 1}, 1, 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('Furuberget {i}, 1940 Bjørkelangen', {i + 1}, 1, 0, '{date}', '{date}')");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionDocumentsAsync(IAppDbContext context, int amount)
        {
            var command = "INSERT INTO MissionDocuments (FileName, MissionId, DocumentTypeId, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (var i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                if (i < (amount - 1)) command = String.Concat(command, $"('https://test.com', {i + 1}, 1, 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('https://test.com', {i + 1}, 2, 0, '{date}', '{date}')");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionImagesAsync(IAppDbContext context, int amount)
        {
            var command = "INSERT INTO MissionImages (FileName, MissionId, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (var i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                if (i < (amount - 1)) command = String.Concat(command, $"('https://test.com', {i + 1}, 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('https://test.com', {i + 1}, 0, '{date}', '{date}')");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetMissionNotesAsync(IAppDbContext context, int amount)
        {
            var command = "INSERT INTO MissionNotes (Content, MissionId, Pinned, Deleted, CreatedAt, UpdatedAt) VALUES ";
            for (var i = 0; i < amount; i++)
            {
                var date = DateTime.Now.AddYears(-i * 2).ToString("yyyy-MM-dd HH:mm:ss");
                var pinned = i % 2 == 0 ? 0 : 1;
                if (i < (amount - 1)) command = String.Concat(command, $"('testnotat', {i + 1}, {pinned}, 0, '{date}', '{date}'),");
                else command = String.Concat(command, $"('testnotat', {i + 1}, {pinned}, 0, '{date}', '{date}')");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
        static async Task SetTimesheetsAsync(IAppDbContext context, int amount)
        {
            var command = "INSERT INTO Timesheets " +
                "(MissionId, StartTime, EndTime, TotalHours, Comment, UserName, Status, Deleted, CreatedAt, UpdatedAt) " +
                "VALUES ";

            var today = DateTimeHelper.Now();

            for (var d = 0; d < amount; d++)
            {
                var startDate = today.AddDays(-d);
                var endDate = startDate.AddHours(4);
                var totalHours = (endDate - startDate).TotalHours;
                var startDateString = startDate.ToString("yyyy-MM-dd HH:mm:ss");
                var endDateString = endDate.ToString("yyyy-MM-dd HH:mm:ss");
                var status = d % 2 == 0 ? 0 : 2;
                if (d < (amount - 1))
                    command = String.Concat(command,
                    $"(1, '{startDateString}','{endDateString}',{totalHours}, 'test', '{Roles.Leader}', {status}, 0, '{startDateString}', '{startDateString}'),");
                else
                    command = String.Concat(command,
                    $"(2, '{startDateString}','{endDateString}',{totalHours}, 'test', '{Roles.Management}', {status}, 0, '{startDateString}', '{startDateString}')");
            }

            await context.Database.ExecuteSqlRawAsync(command);
        }
    }
}
