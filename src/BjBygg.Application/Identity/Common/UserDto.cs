﻿namespace BjBygg.Application.Identity.Common
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public long UpdatedAt { get; set; }
    }
}
