using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Create;
using BjBygg.Application.Identity.Commands.InboundEmailPasswordCommands.Verify;
using BjBygg.Application.Identity.Commands.UserCommands.Create;
using BjBygg.Application.Identity.Commands.UserCommands.NewPassword;
using BjBygg.Application.Identity.Commands.UserCommands.Update;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.Login;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.Logout;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.RefreshToken;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdatePassword;
using BjBygg.Application.Identity.Commands.UserIdentityCommands.UpdateProfile;
using System;
using System.Collections.Generic;

namespace BjBygg.Application.Common
{
    public static class LoggerRequestBlackList
    {
        public static IReadOnlyCollection<Type> BlackList = new Type[]
        {
            typeof(CreateInboundEmailPasswordCommand),
            typeof(VerifyInboundEmailPasswordCommand),
            typeof(CreateUserCommand),
            typeof(UpdateUserCommand),
            typeof(NewPasswordCommand),
            typeof(LoginCommand),
            typeof(LogoutCommand),
            typeof(RefreshTokenCommand),
            typeof(UpdatePasswordCommand),
            typeof(UpdateProfileCommand),
        };
    }
}
