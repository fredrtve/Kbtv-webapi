using BjBygg.Application.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf;
using BjBygg.Application.Application.Commands.MissionCommands.Delete;
using BjBygg.Application.Application.Commands.MissionCommands.DeleteRange;
using BjBygg.Application.Application.Commands.MissionCommands.ToggleMissionFinish;
using BjBygg.Application.Application.Commands.MissionCommands.Update;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Queries.DbSyncQueries;
using BjBygg.Application.Application.Queries.DbSyncQueries.Common;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Verify;
using CleanArchitecture.SharedKernel;
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
        public async Task<ActionResult<DbSyncResponse<MissionDto>>> Sync(MissionSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = RolePermissions.MissionActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<MissionDto>> Create()
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
        public async Task<ActionResult<MissionDto>> CreateFromInboundEmailPdfReport()
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

        [Authorize(Roles = RolePermissions.MissionActions.CreateFromPdf)]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult<MissionDto>> CreateFromPdfReport()
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

        [Authorize(Roles = RolePermissions.MissionActions.Update)]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult<MissionDto>> Update()
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

        [Authorize(Roles = RolePermissions.MissionActions.Update)]
        [HttpGet]
        [Route("api/[controller]/{Id}/[action]")]
        public async Task<ActionResult<bool>> ToggleFinish(ToggleMissionFinishCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = RolePermissions.MissionActions.Delete)]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Delete(DeleteMissionCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.MissionActions.Delete)]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromBody] DeleteRangeMissionCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

    }
}
