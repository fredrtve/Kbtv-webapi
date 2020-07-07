using BjBygg.Application.Common;
using MediatR;

namespace BjBygg.Application.Queries.UserQueries.UserByUserName
{
    public class UserByUserNameQuery : IRequest<UserDto>
    {
        public UserByUserNameQuery() { }
        public string UserName { get; set; }
    }
}
