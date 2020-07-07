using BjBygg.Application.Commands.MissionCommands.Notes;
using BjBygg.Application.Commands.MissionCommands.Notes.Create;
using BjBygg.Application.Commands.MissionCommands.Notes.Update;
using BjBygg.Application.Common;
using BjBygg.Application.Queries.DbSyncQueries;
using BjBygg.Application.Queries.DbSyncQueries.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class MissionNotesController : BaseController
    {
        public MissionNotesController() { }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<DbSyncResponse<MissionNoteDto>> Sync(MissionNoteSyncQuery request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<MissionNoteDto> Create([FromBody] CreateMissionNoteCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<MissionNoteDto> Update([FromBody] UpdateMissionNoteCommand request)
        {
            return await Mediator.Send(request);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<Unit> Delete(DeleteMissionNoteCommand request)
        {
            return await Mediator.Send(request);
        }
    }
}
