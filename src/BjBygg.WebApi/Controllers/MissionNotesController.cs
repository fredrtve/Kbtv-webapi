using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Notes.Create;
using BjBygg.Application.Commands.MissionCommands.Notes.Delete;
using BjBygg.Application.Commands.MissionCommands.Notes.Update;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.MissionNoteQuery;
using BjBygg.Application.Queries.MissionQueries.Note;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers
{
    public class MissionNotesController : BaseController
    {
        private readonly IMediator _mediator;

        public MissionNotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionNoteDto>> Sync([FromBody] MissionNoteSyncQuery query)
        {
            return await _mediator.Send(query);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionNoteDto> GetNote(int id)
        {
            return await _mediator.Send(new MissionNoteByIdQuery() { Id = id });
        }

        [Authorize]
        [HttpPost]
        [Route("api/[controller]/{MissionId}")]
        public async Task<MissionNoteDto> Create([FromBody] CreateMissionNoteCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionNoteDto> Update([FromBody] UpdateMissionNoteCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(DeleteMissionNoteCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
