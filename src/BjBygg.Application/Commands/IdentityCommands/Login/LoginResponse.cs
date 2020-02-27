using BjBygg.Application.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.IdentityCommands.Login
{
    public class LoginResponse
    {
        public UserDto User { get; set; }

        public string Token { get; set; }
    }
}
