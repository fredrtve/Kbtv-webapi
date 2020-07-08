using BjBygg.Application.Queries.DbSyncQueries.SyncAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class SyncAllController : BaseController
    {
        public SyncAllController() { }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<SyncAllResponse>> Get(SyncAllQuery request)
        {
            return await Mediator.Send(request);
        }
    }
}