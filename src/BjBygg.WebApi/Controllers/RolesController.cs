using BjBygg.Application.Queries.UserQueries.RoleList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class RolesController : BaseController
    {
        public RolesController() { }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<IEnumerable<string>>> Index()
        {
            return await Mediator.Send(new RoleListQuery());
        }
    }
}
