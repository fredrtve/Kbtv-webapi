using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BjBygg.Application.Queries.DbSyncQueries.SyncAll;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BjBygg.WebApi.Controllers
{
    public class SyncAllController : BaseController
    {
        public SyncAllController(IMediator mediator, ILogger<SyncAllController> logger) :
            base(mediator, logger) {}

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<SyncAllResponse> Get(SyncAllQuery request)
        {
            request.UserName = User.FindFirstValue(ClaimTypes.Name);
            request.Role = User.FindFirstValue(ClaimTypes.Role);
            return await ValidateAndExecute(request);
        }
    }
}