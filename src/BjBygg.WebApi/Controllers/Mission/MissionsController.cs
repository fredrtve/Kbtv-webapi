using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionCommands.Create;
using BjBygg.Application.Commands.MissionCommands.Delete;
using BjBygg.Application.Commands.MissionCommands.Update;
using BjBygg.Application.Queries.MissionQueries;
using BjBygg.Application.Queries.MissionQueries.Detail;
using BjBygg.Application.Queries.MissionQueries.List;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers.Mission
{
    public class MissionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MissionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Index(string? searchString, int pageId = 0)
        {
            var query = new MissionListQuery() { ItemsPerPage = 8, PageIndex = pageId, SearchString = searchString };
            return Ok(_mediator.Send(query));
        }

        [Authorize(Roles = "Leder, Mellomleder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Create([FromBody] CreateMissionCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{Id}")]
        public async Task<IActionResult> GetMission(int Id)
        {
            var result = await _mediator.Send(new MissionByIdQuery() { Id = Id });
            if (result == null) return NotFound($"Mission does not exist (id = {Id})");
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{Id}/Details")]
        public async Task<IActionResult> GetDetails(int Id)
        {
            var result = await _mediator.Send(new MissionDetailByIdQuery() { Id = Id });
            if (result == null) return NotFound($"Mission does not exist (id = {Id})");
            return Ok(result);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<IActionResult> Update([FromBody] UpdateMissionCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<IActionResult> Delete(DeleteMissionCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result) return NotFound($"Mission does not exist (Id = {command.Id})");

            return Ok(result);
        }
    }
}
