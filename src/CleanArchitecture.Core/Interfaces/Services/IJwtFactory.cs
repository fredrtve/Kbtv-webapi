using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName, string role);
    }
}
