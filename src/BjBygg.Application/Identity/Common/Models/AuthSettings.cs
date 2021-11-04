

using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Identity.Common.Models
{
    public class AuthSettings
    {
        [Required]
        public string SecretKey { get; set; }

        [Required]
        public int RefreshTokenLifeTimeInDays { get; set; }

        [Required]
        public string AdminPassword { get; set; }
    }
}
