using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Queries.UserQueries.RoleList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class RolesController : Controller
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _mediator.Send(new RoleListQuery()));
        }
    }
}
