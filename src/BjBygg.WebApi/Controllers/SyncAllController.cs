using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BjBygg.Application.Queries.DbSyncQueries.SyncAll;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BjBygg.WebApi.Controllers
{
    public class SyncAllController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public SyncAllController(IMediator mediator, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._mediator = mediator;
            this._userManager = userManager;
            this._mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<SyncAllResponse> Get(SyncAllQuery query)
        {
             var userName = User.FindFirstValue(ClaimTypes.Name);
            if (userName == null) throw new BadRequestException("Cant find user");
            var user = await _userManager.FindByNameAsync(userName);
            query.User = _mapper.Map<UserDto>(user);
            query.User.Role = User.FindFirstValue(ClaimTypes.Role);
            return await _mediator.Send(query);
        }
    }
}