using BjBygg.Application.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BjBygg.Application.Queries.UserQueries
{
    public class UserByUserNameQuery : IRequest<UserDto>
    {
        public UserByUserNameQuery()  { }

        [Required]
        public string UserName { get; set; }
    }
}
