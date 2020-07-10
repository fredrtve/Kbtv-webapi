namespace BjBygg.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserName { get; }

        string Role { get; }
    }
}
