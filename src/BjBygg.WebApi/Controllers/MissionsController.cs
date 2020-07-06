using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BjBygg.Application.Commands.InboundEmailPasswordCommands.Verify;
using BjBygg.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Commands.MissionCommands.CreateWithPdf;
using BjBygg.Application.Commands.MissionCommands.Delete;
using BjBygg.Application.Commands.MissionCommands.DeleteRange;
using BjBygg.Application.Commands.MissionCommands.ToggleMissionFinish;
using BjBygg.Application.Commands.MissionCommands.Update;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.MissionQuery;
using BjBygg.Application.Queries.MissionQueries;
using BjBygg.Application.Queries.MissionQueries.Detail;
using BjBygg.Application.Queries.MissionQueries.List;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace BjBygg.WebApi.Controllers
{
    public class MissionsController : BaseController
    {
        public MissionsController(IMediator mediator, ILogger<MissionsController> logger) : 
            base(mediator, logger) {}

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionDto>> Sync(MissionSyncQuery request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<MissionListResponse> Index(MissionListQuery request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/Range")]
        public async Task<IEnumerable<MissionDto>> GetDateRange(MissionByDateRangeQuery request)
        {
            if (request.FromDate == null) request.FromDate = DateTime.UtcNow.AddYears(-25);
            if (request.ToDate == null) request.ToDate = DateTime.UtcNow;
            return await ValidateAndExecute(request);
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

            if (Request.Form.Files.Count() == 0) return await ValidateAndExecute(request);

            var file = Request.Form.Files[0];
            using (var stream = file.OpenReadStream())
            {
                request.Image = new BasicFileStream(stream, Path.GetExtension(file.FileName));

                return await ValidateAndExecute(request);
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
                await ValidateAndExecute(new VerifyInboundEmailPasswordCommand() { Password = emailPassword });

            _logger.LogInformation($"Inbound email authorization accepted? {verifyPasswordResult}");

            if (verifyPasswordResult == false)
                throw new UnauthorizedException($"Unauthorized inbound email from '{fromEmail}'"); 

            if (Request.Form.Files.Count() == 0)
                throw new BadRequestException("No files received");

            using (var streamList = new DisposableList<BasicFileStream>())
            {
                streamList.AddRange(Request.Form.Files.ToList()
                    .Select(x => new BasicFileStream(x.OpenReadStream(), Path.GetExtension(x.FileName))));

                return await ValidateAndExecute(new CreateMissionWithPdfCommand(streamList));
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

                return await ValidateAndExecute(new CreateMissionWithPdfCommand(streamList));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionDto> GetMission(MissionByIdQuery request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{Id}/Details")]
        public async Task<MissionDetailByIdResponse> GetDetails(MissionDetailByIdQuery request)
        {
            return await ValidateAndExecute(request);
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

            if (Request.Form.Files.Count() == 0) return await ValidateAndExecute(request);

            var file = Request.Form.Files[0];

            using (var stream = file.OpenReadStream())
            {
                request.Image = new BasicFileStream(stream, Path.GetExtension(file.FileName));

                return await ValidateAndExecute(request);
            }

        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]/{Id}/[action]")]
        public async Task<bool> ToggleFinish(ToggleMissionFinishCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(DeleteMissionCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeMissionCommand request)
        {
            return await ValidateAndExecute(request);
        }

    }
}
