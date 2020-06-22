using System;
using System.Collections.Generic;
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
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace BjBygg.WebApi.Controllers
{
    public class MissionsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MissionsController> _logger;

        public MissionsController(IMediator mediator, ILogger<MissionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionDto>> Sync(MissionSyncQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<MissionListResponse> Index(bool onlyActive, string? searchString, int pageId = 0)
        {
            var query = new MissionListQuery() { 
                OnlyActive = onlyActive, 
                ItemsPerPage = 8, 
                PageIndex = pageId, 
                SearchString = searchString 
            };

            return await _mediator.Send(query);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/Range")]
        public async Task<IEnumerable<MissionDto>> GetDateRange(MissionByDateRangeQuery query)
        {
            if (query.FromDate == null) query.FromDate = DateTime.UtcNow.AddYears(-25);
            if (query.ToDate == null) query.ToDate = DateTime.UtcNow;
            return await _mediator.Send(query);
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
  
            var command = JsonConvert.DeserializeObject<CreateMissionCommand>(Request.Form["command"], settings);
            command.Image = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;

            if (!TryValidateModel(command))
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
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
            var verifyPasswordResult = await _mediator.Send(new VerifyInboundEmailPasswordCommand() { Password = emailPassword });
            _logger.LogInformation($"Inbound email authorization accepted: {verifyPasswordResult}");
            if (verifyPasswordResult == false) throw new UnauthorizedException($"Unauthorized inbound email from '{fromEmail}'");

            var command = new CreateMissionWithPdfCommand();
            command.Files = Request.Form.Files.Count > 0 ? Request.Form.Files : null;

            if (!TryValidateModel(command))
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<MissionDto> CreateFromPdfReport()
        {
            var command = new CreateMissionWithPdfCommand();
            command.Files = Request.Form.Files.Count > 0 ? Request.Form.Files : null;

            if (!TryValidateModel(command))
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionDto> GetMission(int Id)
        {
            return await _mediator.Send(new MissionByIdQuery() { Id = Id });
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{Id}/Details")]
        public async Task<MissionDetailByIdResponse> GetDetails(int Id)
        {
            return await _mediator.Send(new MissionDetailByIdQuery() { Id = Id });
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

            var command = JsonConvert.DeserializeObject<UpdateMissionCommand>(Request.Form["command"], settings);
            command.Image = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;

            if (!TryValidateModel(command))
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpGet]
        [Route("api/[controller]/{Id}/[action]")]
        public async Task<bool> ToggleFinish(ToggleMissionFinishCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(DeleteMissionCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]/DeleteRange")]
        public async Task<bool> DeleteRange([FromBody] DeleteRangeMissionCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
