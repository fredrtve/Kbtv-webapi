using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.UserQueries.List
{
    public class UserListQuery : IRequest<IEnumerable<UserListItemDto>>
    {
        public string? Role { get; set; }
    }
}
