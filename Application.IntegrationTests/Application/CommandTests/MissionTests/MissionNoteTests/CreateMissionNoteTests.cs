﻿using BjBygg.Application.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Application.Commands.MissionCommands.Notes.Create;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionNoteTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class CreateMissionNoteTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateMissionNoteCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateMissionNote()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new CreateMissionNoteCommand() 
            { 
                MissionId = 1,
                Content = "New content",
                Title = "New title"
            };

            var entity = await SendAsync(command);

            var dbEntity = await FindAsync<MissionNote>(entity.Id);

            dbEntity.Should().NotBeNull();
            dbEntity.Content.Should().Be(command.Content);
            dbEntity.Title.Should().Be(command.Title);
            dbEntity.MissionId.Should().Be(command.MissionId);
            dbEntity.CreatedBy.Should().Be(user.UserName);
            dbEntity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 10000);
        }
    }
}
