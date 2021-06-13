using BjBygg.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Identity.Commands.UserCommands.Create
{
    class ForbiddenRoles
    {
        public static readonly HashSet<string> Value = new HashSet<string> { Roles.Leader, Roles.Admin };     
    }
}
