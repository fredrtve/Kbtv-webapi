using BjBygg.Application.Common;
using CleanArchitecture.Core.Dto;

namespace BjBygg.Application.Commands.IdentityCommands.Login
{
    public class LoginResponse
    {
        public UserDto User { get; set; }
        public AccessToken AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
