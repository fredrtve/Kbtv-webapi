﻿using BjBygg.Application.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Application.Commands.MissionCommands.Notes.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionNoteTests
{
    using static AppTesting;

    public class UpdateMissionNoteTests : AppTestBase
    {
        [Test]
        public void ShouldRequireValidMissionNoteId()
        {
            var command = new UpdateMissionNoteCommand
            {
                Id = 99,
                Content = "New content"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<EntityNotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateMissionNote()
        {
            var user = await RunAsDefaultUserAsync(Roles.Leader);

            var command = new UpdateMissionNoteCommand
            {
                Id = 1,
                Content = "New content",
                Title =  "New title"
            };

            await SendAsync(command);

            var entity = await FindAsync<MissionNote>(1);

            entity.Content.Should().Be(command.Content);
            entity.Title.Should().Be(command.Title);
            entity.UpdatedBy.Should().NotBeNull();
            entity.UpdatedBy.Should().Be(user.UserName);
            entity.UpdatedAt.Should().BeCloseTo(DateTimeHelper.Now(), 1000);
        }
    }
}