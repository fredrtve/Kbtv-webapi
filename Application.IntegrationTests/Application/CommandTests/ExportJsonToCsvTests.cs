using BjBygg.Application.Application.Commands.ExportCsvCommand;
using BjBygg.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests
{
    using static AppTesting;
    //5 x all entities, created 2 yrs apart
    public class ExportJsonToCsvTests : AppTestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new ExportJsonToCsvCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldReturnUri()
        { 
            var command = new ExportJsonToCsvCommand() { 
				JsonObjects = new List<JsonElement>() { new JsonElement() }, 
				PropertyMap = new Dictionary<string, string>() { { "name", "navn" } }
			};

            var uri = await SendAsync(command);

            uri.Should().NotBeNull();
            uri.Should().BeOfType(typeof(Uri));
        }
    }
}
