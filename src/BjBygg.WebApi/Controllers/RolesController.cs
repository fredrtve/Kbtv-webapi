using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BjBygg.Application.Queries.UserQueries.RoleList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class RolesController : BaseController
    {
        public RolesController(IMediator mediator, ILogger<RolesController> logger) :
            base(mediator, logger) {}

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<string>> Index()
        {
            return await _mediator.Send(new RoleListQuery());
        }
    }
}
