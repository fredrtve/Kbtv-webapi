using BjBygg.Application.Application.Commands.MissionCommands;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
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

        [Authorize(Roles = RolePermissions.MissionActions.Create)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Create([FromBody] CreateMissionCommand request)
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

            using (var streamList = new DisposableList<Stream>())
            {
                streamList.AddRange(Request.Form.Files.ToList()
                    .Select(x => x.OpenReadStream()));
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

            using (var streamList = new DisposableList<Stream>())
            {
                streamList.AddRange(Request.Form.Files.ToList()
                    .Select(x => x.OpenReadStream()));

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
        public async Task<ActionResult> UpdateHeaderImage(string id, [FromForm] IFormFile file)
        {
            using var stream = file.OpenReadStream();
            await Mediator.Send(new UpdateMissionHeaderImageCommand() { 
                Id = id, 
                Image = stream,
                FileExtension = Path.GetExtension(file.FileName)
            });
            return NoContent();
        }

        [Authorize(Roles = RolePermissions.MissionActions.DeleteHeaderImage)]
        [HttpPut]
        [Route("api/[controller]/{Id}/[action]")]
        public async Task<ActionResult> DeleteHeaderImage(string id)
        {
            await Mediator.Send( new DeleteMissionHeaderImageCommand(){ Id = id } );
            return NoContent();
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
