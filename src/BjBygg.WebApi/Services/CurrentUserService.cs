using BjBygg.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace BjBygg.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        [ActivatorUtilitiesConstructor]
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            Role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
        }
        public CurrentUserService(string userName, string role) 
        {
            UserName = userName;
            Role = role;
        }

        public string UserName { get; }
        public string Role { get; }
    }
}
