using BjBygg.Application.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Application.Commands.MissionCommands.CreateWithPdf;
using BjBygg.Application.Application.Commands.MissionCommands.Delete;
using BjBygg.Application.Application.Commands.MissionCommands.DeleteRange;
using BjBygg.Application.Application.Commands.MissionCommands.ToggleMissionFinish;
using BjBygg.Application.Application.Commands.MissionCommands.Update;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Commands.MissionCommands.UpdateHeaderImage;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Verify;
using BjBygg.SharedKernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
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

        [Authorize(Roles = RolePermissions.MissionActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<MissionDto>> Create([FromBody] CreateMissionCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult> CreateFromInboundEmailPdfReport()
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
                    .Select(x => new BasicFileStream(x.OpenReadStream(), x.FileName)));
                await Mediator.Send(new CreateMissionWithPdfCommand(streamList));
                return NoContent();
            }
        }

        [Authorize(Roles = RolePermissions.MissionActions.CreateFromPdf)]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult> CreateFromPdfReport()
        {
            if (Request.Form.Files.Count() == 0)
                throw new BadRequestException("No files received");

            using (var streamList = new DisposableList<BasicFileStream>())
            {
                streamList.AddRange(Request.Form.Files.ToList()
                    .Select(x => new BasicFileStream(x.OpenReadStream(), x.FileName)));

                await Mediator.Send(new CreateMissionWithPdfCommand(streamList));
                return NoContent();
            }
        }

        [Authorize(Roles = RolePermissions.MissionActions.Update)]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<ActionResult> Update([FromBody] UpdateMissionCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.MissionActions.UpdateHeaderImage)]
        [HttpPut]
        [Route("api/[controller]/{Id}/[action]")]
        public async Task<ActionResult> UpdateHeaderImage(string id)
        {
            //var settings = new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore,
            //    MissingMemberHandling = MissingMemberHandling.Ignore
            //};

            //var request = JsonConvert.DeserializeObject<UpdateMissionHeaderImageCommand>(Request.Form["command"], settings);
            var request = new UpdateMissionHeaderImageCommand() { Id = id };
            if (Request.Form.Files.Count() == 0)
            {
                await Mediator.Send(request);
                return NoContent();
            }//Let validator throw exception

            var file = Request.Form.Files[0];

            using (var stream = file.OpenReadStream())
            {
                request.Image = new BasicFileStream(stream, file.FileName);

                await Mediator.Send(request);
                return NoContent();
            }

        }

        [Authorize(Roles = RolePermissions.MissionActions.Update)]
        [HttpGet]
        [Route("api/[controller]/{Id}/[action]")]
        public async Task<ActionResult> ToggleFinish(ToggleMissionFinishCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
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
