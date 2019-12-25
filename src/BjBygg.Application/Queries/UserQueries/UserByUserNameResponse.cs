using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.UserQueries
{
    public class UserByUserNameResponse
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
