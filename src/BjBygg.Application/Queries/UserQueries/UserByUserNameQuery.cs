using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.UserQueries
{
    public class UserByUserNameQuery : IRequest<UserByUserNameResponse>
    {
        public UserByUserNameQuery()  { }
        public string UserName { get; set; }
    }
}
