using BjBygg.Application.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.UserQueries
{
    public class UserByUserNameQuery : IRequest<UserDto>
    {
        public UserByUserNameQuery()  { }
        public string UserName { get; set; }
    }
}
