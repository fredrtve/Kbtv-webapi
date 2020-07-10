using BjBygg.Application.Identity.Common;
using MediatR;

namespace BjBygg.Application.Identity.Queries.UserQueries.UserByUserName
{
    public class UserByUserNameQuery : IRequest<UserDto>
    {
        public UserByUserNameQuery() { }
        public string UserName { get; set; }
    }
}
