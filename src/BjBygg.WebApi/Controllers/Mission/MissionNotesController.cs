using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Notes.Create;
using BjBygg.Application.Commands.MissionCommands.Notes.Delete;
using BjBygg.Application.Commands.MissionCommands.Notes.Update;
using BjBygg.Application.Queries.MissionQueries.Note;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers.Mission
{
    public class MissionNotesController : Controller
    {
        private readonly IMediator _mediator;

        public MissionNotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("api/Missions/{MissionId}/[controller]/{Id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            var result = await _mediator.Send(new MissionNoteByIdQuery() { Id = id });
            if (result == null) return NotFound($"Mission note does not exist (id = {id})");
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("api/Missions/{MissionId}/[controller]")]
        public async Task<IActionResult> Create([FromBody] CreateMissionNoteCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/Missions/{MissionId}/[controller]/{Id}")]
        public async Task<IActionResult> Update([FromBody] UpdateMissionNoteCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/Missions/{MissionId}/[controller]/{Id}")]
        public async Task<IActionResult> Delete(DeleteMissionNoteCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result) return NotFound($"Mission note does not exist (Id = {command.Id})");

            return Ok(result);
        }
    }
}
