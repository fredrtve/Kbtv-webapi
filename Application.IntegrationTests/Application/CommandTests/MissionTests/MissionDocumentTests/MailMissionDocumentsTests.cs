using BjBygg.Application.Application.Commands.MissionCommands.Documents.Mail;
using BjBygg.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
            var command = new MailMissionDocumentsCommand() { 
                ToEmail = "test@gmail.com", 
                Ids = new int[] { 1, 2, 3 } 
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow<Exception>();
        }
    }
}
