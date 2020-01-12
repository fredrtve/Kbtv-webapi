using System.Threading.Tasks;
using BjBygg.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Commands.EmployerCommands.Delete;
using BjBygg.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Queries.EmployerQueries.List;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class EmployersController : Controller
    {
        private readonly IMediator _mediator;

        public EmployersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _mediator.Send(new EmployerListQuery()));
        }

        [Authorize(Roles = "Leder, Mellomleder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> Create([FromBody] CreateEmployerCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployerCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<IActionResult> Delete(DeleteEmployerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result) return NotFound($"Employer does not exist (Id = {command.Id})");

            return Ok(result);
        }
    }
}
