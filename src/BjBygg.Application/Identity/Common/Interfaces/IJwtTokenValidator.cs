using System.Security.Claims;

namespace BjBygg.Application.Identity.Common.Interfaces
{
    public interface IJwtTokenValidator
    {
        ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey);
    }
}
