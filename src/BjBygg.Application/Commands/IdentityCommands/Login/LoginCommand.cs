using BjBygg.Application.Queries.UserQueries;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.IdentityCommands.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
