using System.Security.Claims;
using System.Threading.Tasks;
using BjBygg.Application.Queries.DbSyncQueries.SyncAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers
{
    public class SyncAllController : BaseController
    {
        private readonly IMediator _mediator;

        public SyncAllController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<SyncAllResponse> Get(SyncAllQuery query)
        {
            query.UserName = User.FindFirstValue("UserName"); 
            return await _mediator.Send(query);
        }
    }
}