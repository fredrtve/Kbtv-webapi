using BjBygg.Application.Identity.Common;
using BjBygg.Application.Identity.Common.Models;

namespace BjBygg.Application.Identity.Commands.UserIdentityCommands.Login
{
    public class LoginResponse
    {
        public UserDto User { get; set; }
        public AccessToken AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
