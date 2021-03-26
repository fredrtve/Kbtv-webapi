using BjBygg.Application.Application.Queries.DbSyncQueries.SyncAll;
using BjBygg.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Controllers
{
    public class SyncAllController : BaseController
    {
        public SyncAllController() { }

        [ResponseCompression]
        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<SyncAllResponse>> Get(SyncAllQuery request)
        {
            return await Mediator.Send(request);
        }
    }
}