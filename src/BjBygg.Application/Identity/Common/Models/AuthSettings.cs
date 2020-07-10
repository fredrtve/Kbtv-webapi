

namespace BjBygg.Application.Identity.Common.Models
{
    public class AuthSettings
    {
        public string SecretKey { get; set; }
        public int RefreshTokenLifeTimeInDays { get; set; }
    }
}
