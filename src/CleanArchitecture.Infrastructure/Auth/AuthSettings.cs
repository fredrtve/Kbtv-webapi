

namespace CleanArchitecture.Infrastructure.Auth
{
    public class AuthSettings
    {
        public string SecretKey { get; set; }

        public int RefreshTokenLifeTimeInDays { get; set; }
    }
}
