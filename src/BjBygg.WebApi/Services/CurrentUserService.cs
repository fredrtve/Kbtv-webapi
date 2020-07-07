using CleanArchitecture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BjBygg.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            Role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
        }

        public string UserName { get; }
        public string Role { get; }
    }
}
