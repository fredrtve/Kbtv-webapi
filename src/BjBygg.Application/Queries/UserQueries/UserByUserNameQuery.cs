using BjBygg.Application.Shared;
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
