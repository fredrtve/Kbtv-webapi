using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<SyncAllResponse> Post([FromBody] string FromDate)
        {
            return await _mediator.Send(new SyncAllQuery()
            {
                FromDate = FromDate,
                UserName = User.FindFirstValue("UserName")
            });
        }
    }
}