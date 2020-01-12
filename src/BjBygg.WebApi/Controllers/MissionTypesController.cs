using System.Threading.Tasks;
using BjBygg.Application.Commands.MissionTypeCommands.Create;
using BjBygg.Application.Commands.MissionTypeCommands.Delete;
using BjBygg.Application.Commands.MissionTypeCommands.Update;
using BjBygg.Application.Queries.MissionTypeQueries.List;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class MissionTypesController : Controller
    {
        private readonly IMediator _mediator;

        public MissionTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _mediator.Send(new MissionTypeListQuery()));
        }

        [Authorize(Roles = "Leder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Create([FromBody] CreateMissionTypeCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<IActionResult> Update([FromBody] UpdateMissionTypeCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<IActionResult> Delete(DeleteMissionTypeCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result) return NotFound($"Mission type does not exist (Id = {command.Id})");

            return Ok(result);
        }
    }
}
