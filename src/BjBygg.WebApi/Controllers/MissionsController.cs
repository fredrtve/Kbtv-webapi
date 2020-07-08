using BjBygg.Application.Commands.InboundEmailPasswordCommands.Verify;
using BjBygg.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Commands.MissionCommands.CreateWithPdf;
using BjBygg.Application.Commands.MissionCommands.Delete;
using BjBygg.Application.Commands.MissionCommands.DeleteRange;
using BjBygg.Application.Commands.MissionCommands.ToggleMissionFinish;
using BjBygg.Application.Commands.MissionCommands.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class MissionsController : BaseController
    {
        private readonly ILogger<MissionsController> _logger;

        public MissionsController(ILogger<MissionsController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionDto>> Sync(MissionSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder, Mellomleder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<MissionDto> Create()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var request = JsonConvert.DeserializeObject<CreateMissionCommand>(Request.Form["command"], settings);

            if (Request.Form.Files.Count() == 0) return await Mediator.Send(request);

            var file = Request.Form.Files[0];
            using (var stream = file.OpenReadStream())
            {
                request.Image = new BasicFileStream(stream, Path.GetExtension(file.FileName));

                return await Mediator.Send(request);
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<MissionDto> CreateFromInboundEmailPdfReport()
        {
            StringValues fromEmail;
            Request.Form.TryGetValue("from", out fromEmail);

            StringValues toEmail;
            Request.Form.TryGetValue("to", out toEmail);

            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                        $"Schema:{Request.Scheme} " +
                        $"Host: {Request.Host} " +
                        $"Path: {Request.Path} " +
                        $"QueryString: {Request.QueryString} " +
                        $"FromEmail: { fromEmail.ToString() } " +
                        $"ToEmail: { toEmail.ToString() } ");

            var emailPassword = Regex.Match(toEmail, @"(.*?)(?=\@)").Groups[0].Value; //Get password from toEmail (local part)

            var verifyPasswordResult =
                await Mediator.Send(new VerifyInboundEmailPasswordCommand() { Password = emailPassword });

            _logger.LogInformation($"Inbound email authorization accepted? {verifyPasswordResult}");

            if (verifyPasswordResult == false)
                throw new UnauthorizedException($"Unauthorized inbound email from '{fromEmail}'");

            if (Request.Form.Files.Count() == 0)
                throw new BadRequestException("No files received");

            using (var streamList = new DisposableList<BasicFileStream>())
            {
                streamList.AddRange(Request.Form.Files.ToList()
                    .Select(x => new BasicFileStream(x.OpenReadStream(), Path.GetExtension(x.FileName))));
                return await Mediator.Send(new CreateMissionWithPdfCommand(streamList));
            }
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<MissionDto> CreateFromPdfReport()
        {
            if (Request.Form.Files.Count() == 0)
                throw new BadRequestException("No files received");

            using (var streamList = new DisposableList<BasicFileStream>())
            {
                streamList.AddRange(Request.Form.Files.ToList()
                    .Select(x => new BasicFileStream(x.OpenReadStream(), Path.GetExtension(x.FileName))));

                return await Mediator.Send(new CreateMissionWithPdfCommand(streamList));
            }
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionDto> Update()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var request = JsonConvert.DeserializeObject<UpdateMissionCommand>(Request.Form["command"], settings);

            if (Request.Form.Files.Count() == 0) return await Mediator.Send(request);

            var file = Request.Form.Files[0];

            using (var stream = file.OpenReadStream())
            {
                request.Image = new BasicFileStream(stream, Path.GetExtension(file.FileName));

                return await Mediator.Send(request);
            }

        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]/{Id}/[action]")]
        public async Task<bool> ToggleFinish(ToggleMissionFinishCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<Unit> Delete(DeleteMissionCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<Unit> DeleteRange([FromBody] DeleteRangeMissionCommand request)
        {
            return await Mediator.Send(request);
        }

    }
}
