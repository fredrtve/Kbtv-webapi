using BjBygg.Application.Application.Commands.MissionCommands.Images.Mail;
using BjBygg.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;

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
            var command = new MailMissionImagesCommand()
            {
                ToEmail = "test@gmail.com",
                Ids = new string[] { "test" }
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow<Exception>();
        }
    }
}
