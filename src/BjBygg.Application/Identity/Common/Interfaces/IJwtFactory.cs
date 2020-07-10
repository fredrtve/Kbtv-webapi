using BjBygg.Application.Identity.Common.Models;
using System.Threading.Tasks;

namespace BjBygg.Application.Identity.Common.Interfaces
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName, string role);
    }
}
