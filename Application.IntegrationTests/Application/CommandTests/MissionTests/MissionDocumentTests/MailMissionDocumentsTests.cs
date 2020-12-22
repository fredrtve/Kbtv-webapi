using BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail;
using BjBygg.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionDocumentTests
{
    using static AppTesting;
    public class MailMissionDocumentsTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new MailMissionDocumentsCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldThrowNoExceptions()
        {
            var command = new MailMissionDocumentsCommand()
            {
                ToEmail = "test@gmail.com",
                Ids = new string[] { "test", "test2" }
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow<Exception>();
        }
    }
}
