using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.UserQueries.List
{
    public class UserListItemDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int? EmployerId { get; set; }
        public string Role { get; set; }
    }
}
