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
using Microsoft.Extensions.Logging;

namespace BjBygg.WebApi.Controllers
{
    public class MissionNotesController : BaseController
    {
        public MissionNotesController(IMediator mediator, ILogger<MissionNotesController> logger) :
            base(mediator, logger) {}

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionNoteDto>> Sync(MissionNoteSyncQuery request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionNoteDto> GetNote(MissionNoteByIdQuery request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<MissionNoteDto> Create([FromBody] CreateMissionNoteCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionNoteDto> Update([FromBody] UpdateMissionNoteCommand request)
        {
            return await ValidateAndExecute(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(DeleteMissionNoteCommand request)
        {
            return await ValidateAndExecute(request);
        }
    }
}
