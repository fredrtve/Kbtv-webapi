
namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}
