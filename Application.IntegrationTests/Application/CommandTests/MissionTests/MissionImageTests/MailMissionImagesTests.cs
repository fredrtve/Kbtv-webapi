using BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail;
using BjBygg.Application.Application.Commands.MissionCommands.Images.Mail;
using BjBygg.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionImageTests
{
    using static AppTesting;
    public class MailMissionImagesTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new MailMissionImagesCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldThrowNoExceptions()
        {
            var command = new MailMissionImagesCommand() { 
                ToEmail = "test@gmail.com", 
                Ids = new int[] { 1, 2, 3 } 
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow<Exception>();
        }
    }
}
