using BjBygg.Application.Shared;
using BjBygg.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.IntegrationTests.Commands
{
    public class MissionCommands : IntegrationTest
    {
        //[Fact]
        //public async Task Create()
        //{
        //    await AuthenticateAsync();

        //    var response = await TestClient.GetAsync("/api/Missions");
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //    var missions = response.Content.As<IEnumerable<MissionDto>>();
        //    missions.Should().HaveCount(5);
        //}
    }
}
