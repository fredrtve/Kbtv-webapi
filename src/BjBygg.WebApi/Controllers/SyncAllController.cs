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

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<SyncAllResponse>> Get(long? Timestamp)
        {
            return await Mediator.Send(new SyncAllQuery() { Timestamp = Timestamp, InitialSync = false });
        }

        [ResponseCompression]
        [Authorize]
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult<SyncAllResponse>> GetInitial(long? Timestamp)
        {
            return await Mediator.Send(new SyncAllQuery() { Timestamp = Timestamp, InitialSync = true });
        }
    }
}