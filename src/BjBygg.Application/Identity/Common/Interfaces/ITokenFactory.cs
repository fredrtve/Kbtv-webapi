
namespace BjBygg.Application.Identity.Common.Interfaces
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}
