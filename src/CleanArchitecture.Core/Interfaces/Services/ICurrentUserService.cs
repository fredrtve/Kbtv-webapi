namespace CleanArchitecture.Core.Interfaces.Services
{
    public interface ICurrentUserService
    {
        string UserName { get; }

        string Role { get; }
    }
}
