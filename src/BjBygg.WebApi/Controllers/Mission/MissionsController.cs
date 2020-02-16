using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Commands.MissionCommands.Delete;
using BjBygg.Application.Commands.MissionCommands.ToggleMissionFinish;
using BjBygg.Application.Commands.MissionCommands.Update;
using BjBygg.Application.Queries.MissionQueries;
using BjBygg.Application.Queries.MissionQueries.Detail;
using BjBygg.Application.Queries.MissionQueries.List;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers.Mission
{
    public class MissionsController : BaseController
    {
        private readonly IMediator _mediator;

        public MissionsController(IMediator mediator)
        {
            _mediator = mediator;
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
            if (query.FromDate == null) query.FromDate = DateTime.Now.AddYears(-25);
            if (query.ToDate == null) query.ToDate = DateTime.Now;
            return await _mediator.Send(query);
        }

        [Authorize(Roles = "Leder, Mellomleder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<MissionDto> Create([FromBody] CreateMissionCommand command)
        {
            if (!ModelState.IsValid)
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
        public async Task<MissionDto> Update([FromBody] UpdateMissionCommand command)
        {
            if (!ModelState.IsValid)
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
    }
}
