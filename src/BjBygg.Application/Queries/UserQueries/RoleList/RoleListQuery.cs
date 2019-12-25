using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.UserQueries.RoleList
{
    public class RoleListQuery : IRequest<IEnumerable<string>>
    {
    }
}
