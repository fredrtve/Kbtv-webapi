using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Queries.EmployerQueries.List
{
    public class EmployerListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
